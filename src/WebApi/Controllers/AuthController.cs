using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mime;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.RefreshToken.Commands.CreateRefreshToken;
using Application.Features.User.Queries.CheckUserPassword;
using Application.Features.User.Queries.FindByUsername;
using Application.Features.User.Queries.GetAllRoleOfUser;
using Application.Features.User.Queries.IsUserApproved;
using Application.Features.User.Queries.IsUserEmailConfirmed;
using Application.Features.User.Queries.IsUserLockedOut;
using Application.Features.User.Queries.IsUserRemovedByUserId;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.ApiReturnCodes;
using WebApi.Commons;
using WebApi.DTOs.Auth.Incomings;
using WebApi.DTOs.Auth.Outgoings;

namespace WebApi.Controllers;

[Consumes(contentType: MediaTypeNames.Application.Json)]
[Produces(contentType: MediaTypeNames.Application.Json)]
[ApiController]
[Route(template: "api/[controller]")]
public sealed class AuthController : ControllerBase
{
    private readonly ISender _sender;

    public AuthController(ISender sender)
    {
        _sender = sender;
    }

    /// <summary>
    ///     Endpoint for user login.
    /// </summary>
    /// <param name="dto">
    ///     Class contains user credentials.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     A class contains access token, refresh token and
    ///     some user credentials.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST api/Auth/login
    ///     {
    ///         "email": "user@example.com",
    ///         "password": "string",
    ///         "rememberMe": true
    ///     }
    ///
    /// </remarks>
    /// <response code="400"></response>
    /// <response code="404"></response>
    /// <response code="403"></response>
    /// <response code="200"></response>
    [HttpPost(template: "login")]
    public async Task<IActionResult> LoginAsync(
        [FromBody] LoginUserDto dto,
        CancellationToken cancellationToken)
    {
        // Normalize dto.
        dto.NormalizeAllProperties();

        // Find user by username.
        FindByUsernameQuery findByUsernameQuery = new()
        {
            Username = dto.Username
        };

        var foundUser = await _sender.Send(
            request: findByUsernameQuery,
            cancellationToken: cancellationToken);

        // User with username does not exist.
        if (Equals(objA: foundUser, objB: default))
        {
            return NotFound(value: new CommonResponse
            {
                ApiReturnCode = AuthApiReturnCode.USER_IS_NOT_FOUND,
                ErrorMessages = new List<string>(capacity: 1)
                {
                    $"User with username = {dto.Username} is not found."
                }
            });
        }

        // Validate user password.
        CheckUserPasswordQuery checkUserPasswordQuery = new()
        {
            User = foundUser,
            Password = dto.Password
        };

        var isPasswordValid = await _sender.Send(
            request: checkUserPasswordQuery,
            cancellationToken: cancellationToken);

        // Password is not valid.
        if (!isPasswordValid)
        {
            // Is user locked out.
            IsUserLockedOutQuery isUserLockedOutQuery = new()
            {
                User = foundUser,
                Password = dto.Password
            };
            var isUserLockedOut = await _sender.Send(
                request: isUserLockedOutQuery,
                cancellationToken: cancellationToken);

            // User is temporary locked out.
            if (isUserLockedOut)
            {
                return StatusCode(
                    StatusCodes.Status429TooManyRequests,
                    new CommonResponse
                    {
                        ApiReturnCode = AuthApiReturnCode.USER_IS_LOCKED_OUT,
                        ErrorMessages = new List<string>(capacity: 2)
                        {
                            $"User with username = {dto.Username} has been temporarily locked due to entering the wrong password too many times",
                            $"Please try again after 1 minute."
                        }
                    });
            }

            return NotFound(value: new CommonResponse
            {
                ApiReturnCode = AuthApiReturnCode.USER_PASSWORD_IS_NOT_CORRECT,
                ErrorMessages = new List<string>(capacity: 1)
                {
                    "Password is not valid."
                }
            });
        }

        // Has user confirmed account creation email.
        IsUserEmailConfirmedQuery isUserEmailConfirmedQuery = new()
        {
            User = foundUser
        };

        var hasUserConfirmed = await _sender.Send(
            request: isUserEmailConfirmedQuery,
            cancellationToken: cancellationToken);

        // User has not confirmed account creation email.
        if (!hasUserConfirmed)
        {
            return StatusCode(
                statusCode: StatusCodes.Status403Forbidden,
                value: new CommonResponse
                {
                    ApiReturnCode = AuthApiReturnCode.USER_EMAIL_IS_NOT_CONFIRMED,
                    ErrorMessages = new List<string>(capacity: 1)
                    {
                        $"User with username = {dto.Username} has not confirmed account creation email."
                    }
                });
        }

        // Is user approved by admin.
        IsUserApprovedQuery isUserApprovedQuery = new()
        {
            UserId = foundUser.Id
        };

        var isUserApproved = await _sender.Send(
            request: isUserApprovedQuery,
            cancellationToken: cancellationToken);

        // User is not approved by admin.
        if (!isUserApproved)
        {
            return StatusCode(
                statusCode: StatusCodes.Status403Forbidden,
                value: new CommonResponse
                {
                    ApiReturnCode = AuthApiReturnCode.USER_IS_NOT_APPROVED,
                    ErrorMessages = new List<string>(capacity: 1)
                    {
                        $"User with username = {dto.Username} is not approved by admin."
                    }
                });
        }

        // Is user temporarily removed.
        IsUserTemporarilyRemovedByUserIdQuery isUserTemporarilyRemovedByUserIdQuery = new()
        {
            UserId = foundUser.Id
        };

        var isUserTemporarilyRemoved = await _sender.Send(
            request: isUserTemporarilyRemovedByUserIdQuery,
            cancellationToken: cancellationToken);

        // User is temporarily removed.
        if (isUserTemporarilyRemoved)
        {
            return StatusCode(
                statusCode: StatusCodes.Status403Forbidden,
                value: new CommonResponse
                {
                    ApiReturnCode = AuthApiReturnCode.USER_IS_SOFT_REMOVED,
                    ErrorMessages = new List<string>(capacity: 2)
                    {
                        $"User with username = {dto.Username} has already been banned or blocked by Admin.",
                        $"Please contact with admin to recover your account."
                    }
                });
        }

        // Get found user roles.
        GetAllRoleOfUserQuery getAllRoleOfUserQuery = new()
        {
            User = foundUser
        };

        var userRoles = await _sender.Send(
            request: getAllRoleOfUserQuery,
            cancellationToken: cancellationToken);

        // Add new refresh token to the database.
        CreateRefreshTokenCommand createRefreshTokenCommand = new()
        {
            UserClaims =
            [
                new(type: JwtRegisteredClaimNames.Jti,
                    value: Guid.NewGuid().ToString()),
                new(type: JwtRegisteredClaimNames.Sub,
                    value: foundUser.Id.ToString()),
                new(type: ClaimTypes.Role,
                    value: userRoles[default])
            ],
            RememberMe = dto.RememberMe,
            ValueLength = 23
        };

        var refreshTokenValue = await _sender.Send(
            request: createRefreshTokenCommand,
            cancellationToken: cancellationToken);

        // Cannot add new refresh token to the database.
        if (string.IsNullOrWhiteSpace(value: refreshTokenValue))
        {
            return StatusCode(
                statusCode: StatusCodes.Status500InternalServerError,
                value: new CommonResponse()
                {
                    ApiReturnCode = ApiReturnCodes.Base.BaseApiReturnCode.FAILED,
                    ErrorMessages = new List<string>(capacity: 1)
                    {
                        "Database operations failed."
                    }
                });
        }

        // Generate access token.
        //var accessToken = _jwtHandlingService.Generate(claims: createRefreshTokenCommand.UserClaims);

        return Ok(value: new CommonResponse
        {
            Body = new LoginUserSuccessDto
            {
                AccessToken = string.Empty,
                RefreshToken = refreshTokenValue,
                UserCredential = new()
                {
                    Email = foundUser.Email,
                    AvatarUrl = foundUser.AvatarUrl
                }
            }
        });
    }

    // /// <summary>
    // ///     Endpoint for user register.
    // /// </summary>
    // /// <param name="dto">
    // ///     Class contains user register credentials.
    // /// </param>
    // /// <param name="cancellationToken">
    // ///     Automatic initialized token for aborting current operation.
    // /// </param>
    // /// <returns>
    // ///     A message about register operation success
    // /// </returns>
    // /// <remarks>
    // /// Sample request:
    // ///
    // ///     POST api/Auth/register
    // ///     {
    // ///         "email": "user@example.com",
    // ///         "password": "string",
    // ///         "role": "string"
    // ///     }
    // ///
    // /// </remarks>
    // /// <response code="400"></response>
    // /// <response code="409"></response>
    // /// <response code="404"></response>
    // /// <response code="500"></response>
    // /// <response code="200"></response>
    // [HttpPost(template: "register")]
    // public async Task<IActionResult> RegisterAsync(
    //     [FromBody] RegisterAsUserDto dto,
    //     CancellationToken cancellationToken)
    // {
    //     // Normalize dto.
    //     dto.NormalizeAllProperties();

    //     // Does user exist by username.
    //     var isUserFound = await _appUserEntityHandlingService.IsFoundByEmailOrUsernameAsync(
    //         email: dto.Username,
    //         cancellationToken: cancellationToken);

    //     // User with username already exists.
    //     if (isUserFound)
    //     {
    //         return Conflict(error: new CommonResponse
    //         {
    //             ResponseStatusCode = AuthApiCustomStatusCode.USER_IS_EXISTED,
    //             ErrorMessages = new List<string>(capacity: 1)
    //             {
    //                 $"User with username = {dto.Username} already exists"
    //             }
    //         });
    //     }

    //     // Is email real.
    //     var isEmailReal = await _mailHandlingService.IsRealAsync(
    //         email: dto.Username);

    //     // Email is not real.
    //     if (!isEmailReal)
    //     {
    //         return BadRequest(error: new CommonResponse
    //         {
    //             ResponseStatusCode = AuthApiCustomStatusCode.USER_NAME_IS_NOT_A_REAL_EMAIL,
    //             ErrorMessages = new List<string>(capacity: 1)
    //             {
    //                 $"User with username(email) = {dto.Username} is not real"
    //             }
    //         });
    //     }

    //     // Init new user.
    //     AppUserEntity newUser = new()
    //     {
    //         UserName = dto.Username
    //     };

    //     // Is new user password valid.
    //     var isPasswordValid = await _appUserEntityHandlingService.ValidatePasswordAsync(
    //         newUser: newUser,
    //         newPassword: dto.Password);

    //     // Password is not valid.
    //     if (!isPasswordValid)
    //     {
    //         return BadRequest(error: new CommonResponse
    //         {
    //             ResponseStatusCode = AuthApiCustomStatusCode.USER_PASSWORD_IS_NOT_CORRECT,
    //             ErrorMessages = new List<string>(capacity: 1)
    //             {
    //                 "Password is not valid."
    //             }
    //         });
    //     }

    //     // Get user joining status "pending" id.
    //     var pendingUserJoiningStatus = await _userJoiningStatusEntityHandlingService.FindByNameForAuthenticationPurposeAsync(
    //         userJoiningStatusName: "Pending",
    //         cancellationToken: cancellationToken);

    //     newUser.Id = Guid.NewGuid();
    //     newUser.Email = dto.Username;
    //     newUser.UserJoiningStatusId = pendingUserJoiningStatus.Id;
    //     newUser.FirstName = string.Empty;
    //     newUser.LastName = string.Empty;
    //     newUser.Career = string.Empty;
    //     newUser.Workplaces = string.Empty;
    //     newUser.EducationPlaces = string.Empty;
    //     newUser.BirthDay = CustomConstants.App.MIN_DATETIME_SQL;
    //     newUser.HomeAddress = string.Empty;
    //     newUser.AboutMe = string.Empty;
    //     newUser.JoinDate = DateTime.UtcNow;
    //     newUser.AvatarUrl = CustomConstants.App.DEFAULT_AVATAR_URL;
    //     newUser.MajorId = CustomConstants.App.DEFAULT_GUID_FOR_ENUM_TABLES;
    //     newUser.DepartmentId = CustomConstants.App.DEFAULT_GUID_FOR_ENUM_TABLES;
    //     newUser.PositionId = CustomConstants.App.DEFAULT_GUID_FOR_ENUM_TABLES;
    //     newUser.PhoneNumber = string.Empty;

    //     // Create and add user to role.
    //     var dbResult = await _appUserEntityHandlingService
    //         .CreateAndAddUserToRoleAsync(
    //             newUser: newUser,
    //             password: dto.Password,
    //             role: CustomConstants.RoleName.UserRole,
    //             cancellationToken: cancellationToken);

    //     // Cannot create or add user to role.
    //     if (!dbResult)
    //     {
    //         return StatusCode(
    //             statusCode: StatusCodes.Status500InternalServerError,
    //             value: new CommonResponse
    //             {
    //                 ResponseStatusCode = AuthApiCustomStatusCode.FAILED,
    //                 ErrorMessages = new List<string>(capacity: 1)
    //                 {
    //                     "Database operations failed."
    //                 }
    //             });
    //     }

    //     // Init account creation confirmed email token.
    //     var accountCreationConfirmedEmailToken_1 = await _appUserEntityHandlingService.GenerateEmailConfirmationTokenAsync(foundUser: newUser);

    //     // Convert to utf-8 byte array.
    //     var accountCreationConfirmedEmailTokenAsBytes_1 = Encoding.UTF8.GetBytes(s: $"{accountCreationConfirmedEmailToken_1}<token/>{newUser.Id}");

    //     // Convert to base 64 format.
    //     var accountCreationConfirmedEmailTokenAsBase64_1 = WebEncoders.Base64UrlEncode(input: accountCreationConfirmedEmailTokenAsBytes_1);

    //     // Init account creation confirmed email token.
    //     var accountCreationConfirmedEmailToken_2 = await _appUserEntityHandlingService.GenerateEmailConfirmationTokenAsync(foundUser: newUser);

    //     // Convert to utf-8 byte array.
    //     var accountCreationConfirmedEmailTokenAsBytes_2 = Encoding.UTF8.GetBytes(s: $"{accountCreationConfirmedEmailToken_2}<token/>{newUser.Id}");

    //     // Convert to base 64 format.
    //     var accountCreationConfirmedEmailTokenAsBase64_2 = WebEncoders.Base64UrlEncode(input: accountCreationConfirmedEmailTokenAsBytes_2);

    //     // Init new email for account confirmation.
    //     var mailContent = await _mailHandlingService.GetContentAsync(
    //         to: dto.Username,
    //         subject: "Xác nhận tài khoản",
    //         verifyLink1: $"api/auth/confirm-email?token={accountCreationConfirmedEmailTokenAsBase64_1}",
    //         verifyLink2: $"api/auth/confirm-email?token={accountCreationConfirmedEmailTokenAsBase64_2}",
    //         cancellationToken: cancellationToken);

    //     // Send mail to user.
    //     await _mailHandlingService.SendAsync(mailContent: mailContent);

    //     return Ok(value: new CommonResponse
    //     {
    //         ResponseStatusCode = AuthApiCustomStatusCode.ASK_USER_TO_CONFIRM_EMAIL
    //     });
    // }

    // /// <summary>
    // ///     Endpoint for admin register.
    // /// </summary>
    // /// <param name="dto">
    // ///     Class contains user register credentials.
    // /// </param>
    // /// <param name="cancellationToken">
    // ///     Automatic initialized token for aborting current operation.
    // /// </param>
    // /// <returns>
    // ///     A message about register operation success
    // /// </returns>
    // /// <remarks>
    // /// Sample request:
    // ///
    // ///     POST api/Auth/register
    // ///     {
    // ///         "username": "user@example.com",
    // ///         "password": "string",
    // ///         "adminconfirmedkey": "string"
    // ///     }
    // ///
    // /// </remarks>
    // /// <response code="400"></response>
    // /// <response code="409"></response>
    // /// <response code="404"></response>
    // /// <response code="500"></response>
    // /// <response code="200"></response>
    // [HttpPost(template: "admin/register")]
    // public async Task<IActionResult> RegisterAsAdminAsync(
    //     [FromBody] RegisterAsAdminDto dto,
    //     CancellationToken cancellationToken)
    // {
    //     // Normalize dto.
    //     dto.NormalizeAllProperties();

    //     // Admin confirmed key is not correct
    //     if (!dto.AdminConfirmedKey.Equals(value: _adminConfirmedKeyConfig.Value))
    //     {
    //         return Forbid();
    //     }

    //     // Does user exist by username.
    //     var isUserFound = await _appUserEntityHandlingService.IsFoundByEmailOrUsernameAsync(
    //         email: dto.Username,
    //         cancellationToken: cancellationToken);

    //     // User with username already exists.
    //     if (isUserFound)
    //     {
    //         return Conflict(error: new CommonResponse
    //         {
    //             ResponseStatusCode = AuthApiCustomStatusCode.USER_IS_EXISTED,
    //             ErrorMessages = new List<string>(capacity: 1)
    //             {
    //                 $"User with username = {dto.Username} already exists"
    //             }
    //         });
    //     }

    //     // Is email real.
    //     var isEmailReal = await _mailHandlingService.IsRealAsync(
    //         email: dto.Username);

    //     // Email is not real.
    //     if (!isEmailReal)
    //     {
    //         return BadRequest(error: new CommonResponse
    //         {
    //             ResponseStatusCode = AuthApiCustomStatusCode.USER_NAME_IS_NOT_A_REAL_EMAIL,
    //             ErrorMessages = new List<string>(capacity: 1)
    //             {
    //                 $"User with username(email) = {dto.Username} is not real"
    //             }
    //         });
    //     }

    //     // Init new user.
    //     AppUserEntity newUser = new()
    //     {
    //         UserName = dto.Username
    //     };

    //     // Is new user password valid.
    //     var isPasswordValid = await _appUserEntityHandlingService.ValidatePasswordAsync(
    //         newUser: newUser,
    //         newPassword: dto.Password);

    //     // Password is not valid.
    //     if (!isPasswordValid)
    //     {
    //         return BadRequest(error: new CommonResponse
    //         {
    //             ResponseStatusCode = AuthApiCustomStatusCode.USER_PASSWORD_IS_NOT_CORRECT,
    //             ErrorMessages = new List<string>(capacity: 1)
    //             {
    //                 "Password is not valid."
    //             }
    //         });
    //     }

    //     // Get user joining status "pending" id.
    //     var pendingUserJoiningStatus = await _userJoiningStatusEntityHandlingService.FindByNameForAuthenticationPurposeAsync(
    //         userJoiningStatusName: "Pending",
    //         cancellationToken: cancellationToken);

    //     newUser.Id = Guid.NewGuid();
    //     newUser.Email = dto.Username;
    //     newUser.UserJoiningStatusId = pendingUserJoiningStatus.Id;
    //     newUser.FirstName = string.Empty;
    //     newUser.LastName = string.Empty;
    //     newUser.Career = string.Empty;
    //     newUser.Workplaces = string.Empty;
    //     newUser.EducationPlaces = string.Empty;
    //     newUser.BirthDay = CustomConstants.App.MIN_DATETIME_SQL;
    //     newUser.HomeAddress = string.Empty;
    //     newUser.AboutMe = string.Empty;
    //     newUser.JoinDate = DateTime.UtcNow;
    //     newUser.AvatarUrl = CustomConstants.App.DEFAULT_AVATAR_URL;
    //     newUser.MajorId = CustomConstants.App.DEFAULT_GUID_FOR_ENUM_TABLES;
    //     newUser.DepartmentId = CustomConstants.App.DEFAULT_GUID_FOR_ENUM_TABLES;
    //     newUser.PositionId = CustomConstants.App.DEFAULT_GUID_FOR_ENUM_TABLES;
    //     newUser.PhoneNumber = string.Empty;

    //     // Create and add user to role.
    //     var dbResult = await _appUserEntityHandlingService
    //         .CreateAndAddUserToRoleAsync(
    //             newUser: newUser,
    //             password: dto.Password,
    //             role: CustomConstants.RoleName.AdminRole,
    //             cancellationToken: cancellationToken);

    //     // Cannot create or add user to role.
    //     if (!dbResult)
    //     {
    //         return StatusCode(
    //             statusCode: StatusCodes.Status500InternalServerError,
    //             value: new CommonResponse
    //             {
    //                 ResponseStatusCode = AuthApiCustomStatusCode.FAILED,
    //                 ErrorMessages = new List<string>(capacity: 1)
    //                 {
    //                     "Database operations failed."
    //                 }
    //             });
    //     }

    //     // Init account creation confirmed email token.
    //     var accountCreationConfirmedEmailToken_1 = await _appUserEntityHandlingService.GenerateEmailConfirmationTokenAsync(foundUser: newUser);

    //     // Convert to utf-8 byte array.
    //     var accountCreationConfirmedEmailTokenAsBytes_1 = Encoding.UTF8.GetBytes(s: $"{accountCreationConfirmedEmailToken_1}<token/>{newUser.Id}");

    //     // Convert to base 64 format.
    //     var accountCreationConfirmedEmailTokenAsBase64_1 = WebEncoders.Base64UrlEncode(input: accountCreationConfirmedEmailTokenAsBytes_1);

    //     // Init account creation confirmed email token.
    //     var accountCreationConfirmedEmailToken_2 = await _appUserEntityHandlingService.GenerateEmailConfirmationTokenAsync(foundUser: newUser);

    //     // Convert to utf-8 byte array.
    //     var accountCreationConfirmedEmailTokenAsBytes_2 = Encoding.UTF8.GetBytes(s: $"{accountCreationConfirmedEmailToken_2}<token/>{newUser.Id}");

    //     // Convert to base 64 format.
    //     var accountCreationConfirmedEmailTokenAsBase64_2 = WebEncoders.Base64UrlEncode(input: accountCreationConfirmedEmailTokenAsBytes_2);

    //     // Init new email for account confirmation.
    //     var mailContent = await _mailHandlingService.GetContentAsync(
    //         to: dto.Username,
    //         subject: "Xác nhận tài khoản",
    //         verifyLink1: $"api/auth/confirm-email?token={accountCreationConfirmedEmailTokenAsBase64_1}",
    //         verifyLink2: $"api/auth/confirm-email?token={accountCreationConfirmedEmailTokenAsBase64_2}",
    //         cancellationToken: cancellationToken);

    //     // Send mail to user.
    //     await _mailHandlingService.SendAsync(mailContent: mailContent);

    //     return Ok(value: new CommonResponse
    //     {
    //         ResponseStatusCode = AuthApiCustomStatusCode.ASK_USER_TO_CONFIRM_EMAIL
    //     });
    // }

    // /// <summary>
    // ///     Endpoint for changing password
    // /// </summary>
    // /// <param name="dto">
    // ///     A class contains user credentials that used to update user account's password
    // /// </param>
    // /// <param name="cancellationToken">
    // ///     Automatic initialized token for aborting current operation.
    // /// </param>
    // /// <returns>
    // ///     A result message of change password process
    // /// </returns>
    // /// <remarks>
    // /// Sample request:
    // ///
    // ///     PATCH api/Auth/change-password
    // ///     {
    // ///         "email": "user@example.com",
    // ///         "currentPassword": "string",
    // ///         "newPassword": "string"
    // ///     }
    // ///
    // /// </remarks>
    // /// <response code="400"></response>
    // /// <response code="404"></response>
    // /// <response code="403"></response>
    // /// <response code="200"></response>
    // [HttpPatch(template: "change-password")]
    // public async Task<IActionResult> ChangePasswordInForgotPasswordSectionAsync(
    //     [FromBody] ChangePasswordInForgotPasswordSectionDto dto,
    //     CancellationToken cancellationToken)
    // {
    //     // Normalize dto.
    //     dto.NormalizeAllProperties();

    //     // Is current password similar to the new one.
    //     if (dto.CurrentPassword.Equals(value: dto.NewPassword))
    //     {
    //         return BadRequest(error: new CommonResponse
    //         {
    //             ResponseStatusCode = AuthApiCustomStatusCode.CURRENT_AND_NEW_PASSWORD_ARE_THE_SAME,
    //             ErrorMessages = new List<string>(capacity: 1)
    //             {
    //                 "Current password and new password must be different."
    //             }
    //         });
    //     }

    //     // Find user by username.
    //     var foundUser = await _appUserEntityHandlingService.FindByNameAsync(username: dto.Username);

    //     // User with user name does not exist.
    //     if (Equals(objA: foundUser, objB: null))
    //     {
    //         return NotFound(value: new CommonResponse
    //         {
    //             ResponseStatusCode = AuthApiCustomStatusCode.USER_IS_NOT_FOUND,
    //             ErrorMessages = new List<string>(capacity: 1)
    //             {
    //                 $"User with username = {dto.Username} is not found."
    //             }
    //         });
    //     }

    //     // Validate user current password.
    //     var isPasswordValid = await _appUserEntityHandlingService.CheckPasswordAsync(
    //         foundUser: foundUser,
    //         password: dto.CurrentPassword);

    //     // Current password is not valid.
    //     if (!isPasswordValid)
    //     {
    //         // Is user locked out.
    //         var isUserLockedOut = await _appUserEntityHandlingService.IsLockedOutAsync(
    //             foundUser,
    //             dto.CurrentPassword);

    //         // User is temporary locked out.
    //         if (isUserLockedOut)
    //         {
    //             return StatusCode(
    //                 StatusCodes.Status429TooManyRequests,
    //                 new CommonResponse
    //                 {
    //                     ResponseStatusCode = AuthApiCustomStatusCode.USER_IS_LOCKED_OUT,
    //                     ErrorMessages = new List<string>(capacity: 2)
    //                     {
    //                         $"User with username = {dto.Username} has been temporarily locked due to entering the wrong password too many times",
    //                         $"Please try again after 1 minute."
    //                     }
    //                 });
    //         }

    //         return NotFound(value: new CommonResponse
    //         {
    //             ResponseStatusCode = AuthApiCustomStatusCode.USER_PASSWORD_IS_NOT_CORRECT,
    //             ErrorMessages = new List<string>(capacity: 1)
    //             {
    //                 "Current password is not valid."
    //             }
    //         });
    //     }

    //     // Has user confirmed account creation email.
    //     var hasUserConfirmed = await _appUserEntityHandlingService.IsEmailConfirmedAsync(foundUser: foundUser);

    //     // User has not confirmed account creation email.
    //     if (!hasUserConfirmed)
    //     {
    //         return StatusCode(
    //             statusCode: StatusCodes.Status403Forbidden,
    //             value: new CommonResponse
    //             {
    //                 ResponseStatusCode = AuthApiCustomStatusCode.USER_EMAIL_IS_NOT_CONFIRMED,
    //                 ErrorMessages = new List<string>(capacity: 1)
    //                 {
    //                     $"User with username = {dto.Username} has not confirmed account creation email."
    //                 }
    //             });
    //     }

    //     // Is user approved by admin.
    //     var isUserApproved = await _appUserEntityHandlingService.IsApprovedAsync(
    //         userId: foundUser.Id,
    //         cancellationToken: cancellationToken);

    //     // User is not approved by admin.
    //     if (!isUserApproved)
    //     {
    //         return StatusCode(
    //             statusCode: StatusCodes.Status403Forbidden,
    //             value: new CommonResponse
    //             {
    //                 ResponseStatusCode = AuthApiCustomStatusCode.USER_IS_NOT_APPROVED,
    //                 ErrorMessages = new List<string>(capacity: 1)
    //                 {
    //                     $"User with username = {dto.Username} is not approved by admin."
    //                 }
    //             });
    //     }

    //     // Is user soft removed.
    //     var isUserSoftRemoved = await _appUserEntityHandlingService.IsSoftRemovedByIdAsync(
    //         userId: foundUser.Id,
    //         cancellationToken: cancellationToken);

    //     // User is soft removed.
    //     if (isUserSoftRemoved)
    //     {
    //         return StatusCode(
    //             statusCode: StatusCodes.Status403Forbidden,
    //             value: new CommonResponse
    //             {
    //                 ResponseStatusCode = AuthApiCustomStatusCode.USER_IS_SOFT_REMOVED,
    //                 ErrorMessages = new List<string>(capacity: 2)
    //                 {
    //                     $"User with username = {dto.Username} has already been banned or blocked by Admin.",
    //                     $"Please contact with admin to recover your account."
    //                 }
    //             });
    //     }

    //     // Change user password to new password async.
    //     var dbResult = await _appUserEntityHandlingService.ChangePasswordAsync(
    //         foundUser: foundUser,
    //         currentPassword: dto.CurrentPassword,
    //         newPassword: dto.NewPassword,
    //         cancellationToken: cancellationToken);

    //     // Cannot change user current password to new password.
    //     if (!dbResult)
    //     {
    //         return StatusCode(
    //             statusCode: StatusCodes.Status500InternalServerError,
    //             value: new CommonResponse
    //             {
    //                 ResponseStatusCode = AuthApiCustomStatusCode.FAILED,
    //                 ErrorMessages = new List<string>(capacity: 1)
    //                 {
    //                     "Database operations failed."
    //                 }
    //             });
    //     }

    //     return Ok(value: new CommonResponse
    //     {

    //     });
    // }

    // /// <summary>
    // ///     Endpoint for Logout
    // /// </summary>
    // /// <param name="cancellationToken">
    // ///     Automatic initialized token for aborting current operation.
    // /// </param>
    // /// <remarks>
    // /// Sample request:
    // ///
    // ///     POST api/Auth/logout
    // ///
    // /// </remarks>
    // /// <response code="500"></response>
    // /// <response code="400"></response>
    // /// <response code="200"></response>
    // [Authorize]
    // [HttpPost(template: "logout")]
    // public async Task<IActionResult> LogoutAsync(CancellationToken cancellationToken)
    // {
    //     // Get jti claim from access token.
    //     var jtiClaim = User.FindFirstValue(claimType: JwtRegisteredClaimNames.Jti);

    //     // Parse jti claim into access token id.
    //     var accessTokenId = Guid.Parse(input: jtiClaim);

    //     // Find refresh token by the access token id;
    //     var foundRefreshToken = await _refreshTokenEntityHandlingService.FindByAccessTokenIdForLogoutPurposeAsync(
    //         accessTokenId: accessTokenId,
    //         cancellationToken: cancellationToken);

    //     // Remove found refresh token.
    //     var dbResult = await _refreshTokenEntityHandlingService.RemoveAsync(
    //         foundRefreshToken: foundRefreshToken,
    //         cancellationToken: cancellationToken);

    //     // Cannot remove found refresh token.
    //     if (!dbResult)
    //     {
    //         return StatusCode(
    //             statusCode: StatusCodes.Status500InternalServerError,
    //             value: new CommonResponse
    //             {
    //                 ResponseStatusCode = AuthApiCustomStatusCode.FAILED,
    //                 ErrorMessages = new List<string>(capacity: 1)
    //                 {
    //                     "Database operations failed."
    //                 }
    //             });
    //     }

    //     return Ok(value: new CommonResponse
    //     {

    //     });
    // }

    // /// <summary>
    // ///     Endpoint for confirm email
    // /// </summary>
    // /// <param name="base64AccountCreationConfirmedEmailToken">
    // ///     A token value that will be used for email confirmation
    // /// </param>
    // /// <returns>
    // ///     A result message of email confirmation process
    // /// </returns>
    // /// <remarks>
    // /// Sample request:
    // ///
    // ///     GET api/Auth/confirm-email?token={base64AccountCreationConfirmedEmailToken}
    // ///
    // /// </remarks>
    // /// <response code="500"></response>
    // /// <response code="400"></response>
    // /// <response code="404"></response>
    // /// <response code="403"></response>
    // /// <response code="200"></response>
    // [HttpGet(template: "confirm-email")]
    // public async Task<IActionResult> ConfirmEmailAsync(
    //     [FromQuery(Name = "token")]
    //     [Required]
    //     [IsStringNotNull]
    //         string base64AccountCreationConfirmedEmailToken)
    // {
    //     // Normalize token.
    //     base64AccountCreationConfirmedEmailToken = base64AccountCreationConfirmedEmailToken.Trim();

    //     string responseBody;
    //     string responseTemplateName;
    //     byte[] decodedAccountCreationConfirmedEmailToken;

    //     try
    //     {
    //         // Decode token.
    //         decodedAccountCreationConfirmedEmailToken = WebEncoders.Base64UrlDecode(
    //             input: base64AccountCreationConfirmedEmailToken);
    //     }
    //     catch
    //     {
    //         responseTemplateName = "AccountConfirmedUrlIsNotValidTemplate.html";
    //         responseBody = await GenerateHtmlResponseAsync();

    //         return Content(
    //             content: responseBody,
    //             contentType: MediaTypeNames.Text.Html);
    //     }

    //     // Extract decoded token.
    //     var tokens = Encoding.UTF8
    //         .GetString(bytes: decodedAccountCreationConfirmedEmailToken)
    //         .Split(separator: "<token/>");

    //     // Get the account creation confirmed email token.
    //     var accountCreationConfirmedEmailToken = tokens[default];

    //     // Get the user id.
    //     var userId = Guid.Parse(input: tokens[1]);

    //     // Find user by user id.
    //     var foundUser = await _appUserEntityHandlingService.FindByIdAsync(userId: userId);

    //     // User with user id does not exist.
    //     if (Equals(objA: foundUser, objB: null))
    //     {
    //         return NotFound(value: new CommonResponse
    //         {
    //             ResponseStatusCode = AuthApiCustomStatusCode.USER_IS_NOT_FOUND,
    //             ErrorMessages = new List<string>(capacity: 1)
    //             {
    //                 $"User is not found."
    //             }
    //         });
    //     }

    //     // Has user confirmed account creation email.
    //     var hasUserConfirmed = await _appUserEntityHandlingService.IsEmailConfirmedAsync(foundUser: foundUser);

    //     // User has confirmed account creation email.
    //     if (hasUserConfirmed)
    //     {
    //         responseTemplateName = "UserHasAlreadyConfirmedAccountSuccessfullyTemplate.html";
    //         responseBody = await GenerateHtmlResponseAsync();

    //         return Content(
    //             content: responseBody,
    //             contentType: MediaTypeNames.Text.Html);
    //     }

    //     // Confirm user account creation.
    //     var dbResult = await _appUserEntityHandlingService.ConfirmEmailAsync(
    //         foundUser: foundUser,
    //         emailConfirmToken: accountCreationConfirmedEmailToken);

    //     // Cannot confirm user account creation.
    //     if (!dbResult)
    //     {
    //         responseTemplateName = "ServerErrorTemplate.html";
    //         responseBody = await GenerateHtmlResponseAsync();

    //         return Content(
    //             content: responseBody,
    //             contentType: MediaTypeNames.Text.Html);
    //     }

    //     // Generate html response.
    //     responseTemplateName = "UserHasConfirmedAccountSuccessfullyTemplate.html";
    //     responseBody = await GenerateHtmlResponseAsync();

    //     return Content(
    //         content: responseBody,
    //         contentType: MediaTypeNames.Text.Html);

    //     async Task<string> GenerateHtmlResponseAsync()
    //     {
    //         //Construct html template path.
    //         var userHasConfirmedAccountSuccessfullyHtmlPath = Path.Combine(
    //             path1: "CreateUserAccount",
    //             path2: responseTemplateName);

    //         var fullPath = Path.Combine(
    //             path1: _webHostEnvironment.WebRootPath,
    //             path2: userHasConfirmedAccountSuccessfullyHtmlPath);

    //         //Get the html template from file.
    //         return await System.IO.File.ReadAllTextAsync(
    //             path: fullPath,
    //             cancellationToken: cancellationToken);
    //     }
    // }

    // /// <summary>
    // ///     An endpoint that used for email resend purpose
    // /// </summary>
    // /// <param name="dto">
    // /// </param>
    // /// <param name="cancellationToken">
    // ///     Automatic initialized token for aborting current operation.
    // /// </param>
    // /// <returns>
    // ///     The List of AppUsers
    // /// </returns>
    // /// <remarks>
    // /// Sample request:
    // ///
    // ///     POST api/Auth/resend-email
    // ///     {
    // ///         "username": "string-value"
    // ///     }
    // ///
    // /// </remarks>
    // /// <response code="400"></response>
    // /// <response code="403"></response>
    // /// <response code="404"></response>
    // /// <response code="200"></response>
    // [HttpPost(template: "resend-email")]
    // public async Task<IActionResult> ResendEmailAsync(
    //     [FromBody] ResendEmailDto dto,
    //     CancellationToken cancellationToken)
    // {
    //     // Normalize dto.
    //     dto.NormalizeAllProperties();

    //     // Find user by user name.
    //     var foundUser = await _appUserEntityHandlingService.FindByNameAsync(username: dto.Username);

    //     // User with user name does not exist.
    //     if (Equals(objA: foundUser, objB: null))
    //     {
    //         return NotFound(value: new CommonResponse
    //         {
    //             ResponseStatusCode = AuthApiCustomStatusCode.USER_IS_NOT_FOUND,
    //             ErrorMessages = new List<string>(capacity: 1)
    //             {
    //                 $"User with username = {dto.Username} is not found."
    //             }
    //         });
    //     }

    //     // Has user confirm account creation email.
    //     var hasUserConfirmed = await _appUserEntityHandlingService.IsEmailConfirmedAsync(foundUser: foundUser);

    //     // User has confirmed account creation email.
    //     if (hasUserConfirmed)
    //     {
    //         return StatusCode(
    //             statusCode: StatusCodes.Status403Forbidden,
    //             value: new CommonResponse
    //             {
    //                 ResponseStatusCode = AuthApiCustomStatusCode.USER_HAS_CONFIRMED_EMAIL,
    //                 ErrorMessages = new List<string>(capacity: 1)
    //                 {
    //                     $"User with username = {foundUser.Email} has already confirmed account creation email."
    //                 }
    //             });
    //     }

    //     // Init account creation confirmed email token.
    //     var accountCreationConfirmedEmailToken_1 = await _appUserEntityHandlingService.GenerateEmailConfirmationTokenAsync(foundUser: foundUser);

    //     // Convert to utf-8 byte array.
    //     var accountCreationConfirmedEmailTokenAsBytes_1 = Encoding.UTF8.GetBytes(s: $"{accountCreationConfirmedEmailToken_1}<token/>{foundUser.Id}");

    //     // Convert to base 64 format.
    //     var accountCreationConfirmedEmailTokenAsBase64_1 = WebEncoders.Base64UrlEncode(input: accountCreationConfirmedEmailTokenAsBytes_1);

    //     // Init account creation confirmed email token.
    //     var accountCreationConfirmedEmailToken_2 = await _appUserEntityHandlingService.GenerateEmailConfirmationTokenAsync(foundUser: foundUser);

    //     // Convert to utf-8 byte array.
    //     var accountCreationConfirmedEmailTokenAsBytes_2 = Encoding.UTF8.GetBytes(s: $"{accountCreationConfirmedEmailToken_2}<token/>{foundUser.Id}");

    //     // Convert to base 64 format.
    //     var accountCreationConfirmedEmailTokenAsBase64_2 = WebEncoders.Base64UrlEncode(input: accountCreationConfirmedEmailTokenAsBytes_2);

    //     // Init new email for account confirmation.
    //     var mailContent = await _mailHandlingService.GetContentAsync(
    //         to: dto.Username,
    //         subject: "Xác nhận tài khoản",
    //         verifyLink1: $"api/auth/confirm-email?token={accountCreationConfirmedEmailTokenAsBase64_1}",
    //         verifyLink2: $"api/auth/confirm-email?token={accountCreationConfirmedEmailTokenAsBase64_2}",
    //         cancellationToken: cancellationToken);

    //     // Send mail to user.
    //     await _mailHandlingService.SendAsync(mailContent: mailContent);

    //     return Ok(value: new CommonResponse
    //     {
    //         ResponseStatusCode = AuthApiCustomStatusCode.ASK_USER_TO_CONFIRM_EMAIL
    //     });
    // }

    // /// <summary>
    // ///     Refresh and generate new AccessToken and RefreshToken
    // ///     by input refreshToken value.
    // /// </summary>
    // /// <param name="dto">
    // ///     A class contains refreshToken value that will be used to generate
    // ///     new accessToken and refreshToken.
    // /// </param>
    // /// <param name="cancellationToken">
    // ///     Automatic initialized token for aborting current operation.
    // /// </param>
    // /// <returns>
    // ///     An object contains newly generated AccessToken and RefreshToken.
    // /// </returns>
    // /// <remarks>
    // /// Sample request:
    // ///
    // ///     POST api/Auth/refresh-access-token
    // ///     {
    // ///         "refreshToken": "token-value"
    // ///     }
    // ///
    // /// </remarks>
    // /// <response code="200"></response>
    // [Authorize(Policy = CustomConstants.AuthorizePolicyName.RefreshAccessTokenRequirement)]
    // [HttpPost(template: "refresh-access-token")]
    // public async Task<IActionResult> RefreshAccessTokenAsync(
    //     [FromBody] RefreshAccessTokenDto dto,
    //     CancellationToken cancellationToken)
    // {
    //     // Normalize dto.
    //     dto.NormalizeAllProperties();

    //     // Get exp claim from access token.
    //     var expClaim = User.FindFirstValue(claimType: JwtRegisteredClaimNames.Exp);

    //     // Is access token expired.
    //     var isAccessTokenExpired = _jwtHandlingService.IsExpired(accessTokenExpiredTime: expClaim);

    //     // Access token is not expired.
    //     if (!isAccessTokenExpired)
    //     {
    //         return BadRequest(error: new CommonResponse
    //         {
    //             ResponseStatusCode = AuthApiCustomStatusCode.ACCESS_TOKEN_IS_NOT_EXPIRED,
    //             ErrorMessages = new List<string>(capacity: 1)
    //             {
    //                 "Access token is not expired."
    //             }
    //         });
    //     }

    //     // Find refresh token by its value.
    //     var foundRefreshToken = await _refreshTokenEntityHandlingService.FindByRefreshTokenValueAsync(
    //         refreshTokenValue: dto.RefreshToken,
    //         cancellationToken: cancellationToken);

    //     // Refresh token does not exist.
    //     if (Equals(objA: foundRefreshToken, objB: null))
    //     {
    //         return NotFound(value: new CommonResponse
    //         {
    //             ResponseStatusCode = AuthApiCustomStatusCode.REFRESH_TOKEN_IS_NOT_FOUND,
    //             ErrorMessages = new List<string>(capacity: 1)
    //             {
    //                 "Refresh token is not found"
    //             }
    //         });
    //     }

    //     // Get jti claim from access token.
    //     var jtiClaim = User.FindFirstValue(claimType: JwtRegisteredClaimNames.Jti);

    //     // Parse it into access token id.
    //     var accessTokenId = Guid.Parse(input: jtiClaim);

    //     // Access token id from the input access token is
    //     // not correct with the one that is found in refresh token.
    //     if (foundRefreshToken.AccessTokenId != accessTokenId)
    //     {
    //         return StatusCode(
    //             statusCode: StatusCodes.Status403Forbidden,
    //             value: new CommonResponse
    //             {
    //                 ResponseStatusCode = AuthApiCustomStatusCode.REFRESH_TOKEN_IS_DENIED,
    //                 ErrorMessages = new List<string>(capacity: 1)
    //                 {
    //                     "Input refresh token is not belong to input access token."
    //                 }
    //             });
    //     }

    //     // Refresh token is expired.
    //     if (DateTime.UtcNow > foundRefreshToken.ExpiredDate)
    //     {
    //         return Unauthorized(value: new CommonResponse
    //         {
    //             ResponseStatusCode = AuthApiCustomStatusCode.REFRESH_TOKEN_IS_EXPIRED,
    //             ErrorMessages = new List<string>(capacity: 1)
    //             {
    //                 "Refresh token is expired."
    //             }
    //         });
    //     }

    //     // Init list of user claims.
    //     List<Claim> userClaims = new(capacity: 3)
    //     {
    //         new(
    //             type: JwtRegisteredClaimNames.Jti,
    //             value: Guid.NewGuid().ToString()),
    //         new(
    //             type: JwtRegisteredClaimNames.Sub,
    //             value: User.FindFirstValue(claimType: JwtRegisteredClaimNames.Sub)),
    //         new(
    //             type: ClaimTypes.Role,
    //             value: User.FindFirstValue(claimType: "role"))
    //     };

    //     foundRefreshToken.Value = CommonMethods.RandomStringGenerator(length: 23);
    //     foundRefreshToken.AccessTokenId = Guid.Parse(input: userClaims[default].Value);

    //     // Update found refresh token.
    //     var dbResult = await _refreshTokenEntityHandlingService.UpdateAsync(
    //         refreshTokenEntity: foundRefreshToken,
    //         cancellationToken: cancellationToken);

    //     // Cannot update found refresh token.
    //     if (!dbResult)
    //     {
    //         return StatusCode(
    //             statusCode: StatusCodes.Status500InternalServerError,
    //             value: new CommonResponse
    //             {
    //                 ResponseStatusCode = AuthApiCustomStatusCode.FAILED,
    //                 ErrorMessages = new List<string>(capacity: 1)
    //                 {
    //                     "Database operations failed."
    //                 }
    //             });
    //     }

    //     // Generate access token.
    //     var accessToken = _jwtHandlingService.Generate(claims: userClaims);

    //     return Ok(value: new CommonResponse
    //     {
    //         Body = new RefreshAccessTokenSuccessDto
    //         {
    //             AccessToken = accessToken,
    //             RefreshToken = foundRefreshToken.Value
    //         }
    //     });
    // }
}
