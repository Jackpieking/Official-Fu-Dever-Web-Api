using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using FuDever.Application.Interfaces.Authentication.Jwt;
using FuDever.Domain.EntityBuilders.RefreshToken;
using FuDever.Domain.Specifications.Others.Interfaces;
using FuDever.Domain.UnitOfWorks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;

namespace FuDever.Application.Features.Auth.Login;

public sealed class LoginRequestHandler : IRequestHandler<
    LoginRequest,
    LoginResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;
    private readonly UserManager<Domain.Entities.User> _userManager;
    private readonly SignInManager<Domain.Entities.User> _signInManager;
    private readonly IRefreshTokenHandler _refreshTokenHandler;
    private readonly IAccessTokenHandler _accessTokenHandler;

    public LoginRequestHandler(
        IUnitOfWork unitOfWork,
        ISuperSpecificationManager superSpecificationManager,
        UserManager<Domain.Entities.User> userManager,
        SignInManager<Domain.Entities.User> signInManager,
        IRefreshTokenHandler refreshTokenHandler,
        IAccessTokenHandler accessTokenHandler)
    {
        _unitOfWork = unitOfWork;
        _superSpecificationManager = superSpecificationManager;
        _userManager = userManager;
        _signInManager = signInManager;
        _refreshTokenHandler = refreshTokenHandler;
        _accessTokenHandler = accessTokenHandler;
    }

    /// <summary>
    ///     Entry of new request handler.
    /// </summary>
    /// <param name="request">
    ///     Request model.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     A task containing the response.
    /// </returns>
    public async Task<LoginResponse> Handle(
        LoginRequest request,
        CancellationToken cancellationToken)
    {
        // Find user by username.
        var foundUser = await GetUserByUsernameQueryAsync(username: request.Username);

        // User with username does not exist.
        if (Equals(objA: foundUser, objB: default))
        {
            return new()
            {
                StatusCode = LoginResponseStatusCode.USER_IS_NOT_FOUND
            };
        }

        // Validate user password.
        var isPasswordValid = await ValidateUserPasswordAsync(
            user: foundUser,
            userPassword: request.Password);

        // Password is not valid.
        if (!isPasswordValid)
        {
            // Is user locked out.
            var isUserLockedOut = await IsUserLockedOutQueryAsync(
                user: foundUser,
                userPassword: request.Password);

            // User is temporary locked out.
            if (isUserLockedOut)
            {
                return new()
                {
                    StatusCode = LoginResponseStatusCode.USER_IS_LOCKED_OUT
                };
            }

            return new()
            {
                StatusCode = LoginResponseStatusCode.USER_PASSWORD_IS_NOT_CORRECT
            };
        }

        // Has user confirmed account creation email.
        var hasUserConfirmed = await IsUserEmailConfirmedAsync(user: foundUser);

        // User has not confirmed account creation email.
        if (!hasUserConfirmed)
        {
            return new()
            {
                StatusCode = LoginResponseStatusCode.USER_EMAIL_IS_NOT_CONFIRMED
            };
        }

        // Is user approved by admin.
        var hasUserApproved = await IsUserApprovedQueryAsync(
            userId: foundUser.Id,
            cancellationToken: cancellationToken);

        // User is not approved by admin.
        if (!hasUserApproved)
        {
            return new()
            {
                StatusCode = LoginResponseStatusCode.USER_IS_NOT_APPROVED
            };
        }

        // Is user soft removed.
        var isUserTemporarilyRemoved = await IsUserTemporarilyRemovedQueryAsync(
            userId: foundUser.Id,
            cancellationToken: cancellationToken);

        // User is soft removed.
        if (isUserTemporarilyRemoved)
        {
            return new()
            {
                StatusCode = LoginResponseStatusCode.USER_IS_TEMPORARILY_REMOVED
            };
        }

        // Get found user roles.
        var foundUserRoles = await GetAllUserRolesQueryAsync(user: foundUser);

        // Init list of user claims.
        List<Claim> userClaims =
        [
            new(type: JwtRegisteredClaimNames.Jti,
                value: Guid.NewGuid().ToString()),
            new(type: JwtRegisteredClaimNames.Sub,
                value: foundUser.Id.ToString()),
            new(type: ClaimTypes.Role,
                value: foundUserRoles[default])
        ];

        // Create new refresh token.
        RefreshTokenForNewRecordBuilder builder = new();

        var newRefreshToken = builder
            .WithId(refreshTokenId: Guid.NewGuid())
            .WithCreatedBy(refreshTokenCreatedBy: Guid.Parse(input: userClaims
                .First(predicate: claim => claim.Type.Equals(
                    value: JwtRegisteredClaimNames.Sub))
                .Value))
            .WithAccessTokenId(accessTokenId: Guid.Parse(input: userClaims
                .First(predicate: claim => claim.Type.Equals(
                    value: JwtRegisteredClaimNames.Jti))
                .Value))
            .WithExpiredDate(refreshTokenExpiredDate: request.RememberMe ?
                DateTime.UtcNow.AddDays(value: 7) :
                DateTime.UtcNow.AddDays(value: 3))
            .WithCreatedAt(refreshTokenCreatedAt: DateTime.UtcNow)
            .WithValue(refreshTokenValue: _refreshTokenHandler.Generate(length: 15))
            .Complete();

        // Add new refresh token to the database.
        var dbResult = await CreateReFreshTokenCommandAsync(
            newRefreshToken: newRefreshToken,
            cancellationToken: cancellationToken);

        // Cannot add new refresh token to the database.
        if (!dbResult)
        {
            return new()
            {
                StatusCode = LoginResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        // Generate access token.
        var newAccessToken = _accessTokenHandler.Generate(claims: userClaims);

        return new()
        {
            StatusCode = LoginResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken.Value,
                User = new()
                {
                    Email = foundUser.Email,
                    AvatarUrl = foundUser.AvatarUrl
                }
            }
        };
    }

    #region Others
    /// <summary>
    ///     Validate if the given user's password
    ///     matches the provided password.
    /// </summary>
    /// <param name="user">
    ///     The user to check the password for.
    /// </param>
    /// <param name="userPassword">
    ///     The password to check against the user's password.
    /// </param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result
    ///     contains a boolean value indicating whether the password matches
    ///     the user's password.
    /// </returns>
    private Task<bool> ValidateUserPasswordAsync(
        Domain.Entities.User user,
        string userPassword)
    {
        return _userManager.CheckPasswordAsync(
            user: user,
            password: userPassword);
    }

    /// <summary>
    ///     Checks if the email of a given user is confirmed.
    /// </summary>
    /// <param name="user">
    ///     The user to check.
    /// </param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result
    ///     contains a boolean value indicating whether the email of the user
    ///     is confirmed or not.
    /// </returns>
    private Task<bool> IsUserEmailConfirmedAsync(Domain.Entities.User user)
    {
        return _userManager.IsEmailConfirmedAsync(user: user);
    }

    #endregion

    #region Queries
    /// <summary>
    ///     Retrieves a user from the database based on the provided username.
    /// </summary>
    /// <param name="username">
    ///     The username of the user to retrieve.
    /// </param>
    /// <returns>
    ///     A task that represents the asynchronous operation.
    ///     The task result contains the user entity.
    /// </returns>
    private Task<Domain.Entities.User> GetUserByUsernameQueryAsync(string username)
    {
        return _userManager.FindByNameAsync(userName: username);
    }

    /// <summary>
    ///     Retrieves a boolean indicating if the user is locked out
    ///     based on the provided user and password.
    /// </summary>
    /// <param name="user">
    ///     The user entity.
    /// </param>
    /// <param name="userPassword">
    ///     The user's password.
    /// </param>
    /// <returns>
    ///     A task that represents the asynchronous operation.
    ///     The task result contains a boolean indicating if the user
    ///     is locked out or not.
    /// </returns>
    private async Task<bool> IsUserLockedOutQueryAsync(
        Domain.Entities.User user,
        string userPassword)
    {
        var result = await _signInManager.CheckPasswordSignInAsync(
            user: user,
            password: userPassword,
            lockoutOnFailure: true);

        return result.IsLockedOut;
    }

    /// <summary>
    ///     Retrieves a boolean indicating if the user is approved
    ///     based on the provided user id.
    /// </summary>
    /// <param name="userId">
    ///     The id of the user.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     A task that represents the asynchronous operation.
    ///     The task result contains a boolean indicating if the user
    ///     is approved or not.
    /// </returns>
    private async Task<bool> IsUserApprovedQueryAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        const string UserApprovedStatus = "Approved";

        var foundUser = await _unitOfWork.UserRepository.FindBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.User.UserAsNoTrackingSpecification,
                _superSpecificationManager.User.UserByIdSpecification(userId: userId),
                _superSpecificationManager.User.SelectFieldsFromUserSpecification.Ver1()
            ],
            cancellationToken: cancellationToken);

        return foundUser.UserJoiningStatus.Type.Equals(value: UserApprovedStatus);
    }

    /// <summary>
    ///     Retrieves a boolean indicating if the
    ///     user is temporarily removed based on
    ///     the provided user id.
    /// </summary>
    /// <param name="userId">
    ///     The id of the user.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     A task that represents the asynchronous operation.
    ///     The task result contains a boolean indicating if the user
    ///     is temporarily removed or not.
    /// </returns>
    private Task<bool> IsUserTemporarilyRemovedQueryAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        return _unitOfWork.UserRepository.IsFoundBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.User.UserByIdSpecification(userId: userId),
                _superSpecificationManager.User.UserTemporarilyRemovedSpecification
            ],
            cancellationToken: cancellationToken);
    }

    /// <summary>
    ///     Retrieves a list of roles for the given user.
    /// </summary>
    /// <param name="user">The user entity.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation.
    ///     The task result contains a list of strings,
    ///     each string being a user's role.
    /// </returns>
    private Task<IList<string>> GetAllUserRolesQueryAsync(Domain.Entities.User user)
    {
        // Retrieves a list of roles for the given user using the user manager.
        // The user manager's GetRolesAsync method is used to get the roles
        // associated with the user.

        // The task result contains a list of strings, each string being a user's role.
        return _userManager.GetRolesAsync(user: user);
    }
    #endregion

    #region Commands
    private async Task<bool> CreateReFreshTokenCommandAsync(
        Domain.Entities.RefreshToken newRefreshToken,
        CancellationToken cancellationToken)
    {
        if (Equals(objA: newRefreshToken, objB: default))
        {
            return false;
        }

        var executedTransactionResult = false;

        await _unitOfWork
            .CreateExecutionStrategy()
            .ExecuteAsync(operation: async () =>
            {
                try
                {
                    await _unitOfWork.CreateTransactionAsync(cancellationToken: cancellationToken);

                    await _unitOfWork.RefreshTokenRepository.AddAsync(
                        newEntity: newRefreshToken,
                        cancellationToken: cancellationToken);

                    await _unitOfWork.SaveToDatabaseAsync(cancellationToken: cancellationToken);

                    await _unitOfWork.CommitTransactionAsync(cancellationToken: cancellationToken);

                    executedTransactionResult = true;
                }
                catch
                {
                    await _unitOfWork.RollBackTransactionAsync(cancellationToken: cancellationToken);
                }
                finally
                {
                    await _unitOfWork.DisposeTransactionAsync();
                }
            });

        return executedTransactionResult;
    }
    #endregion
}
