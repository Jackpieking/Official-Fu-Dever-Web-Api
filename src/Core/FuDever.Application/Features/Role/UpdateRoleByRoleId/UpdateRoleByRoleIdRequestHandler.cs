using FuDever.Domain.Specifications.Others.Interfaces;
using FuDever.Domain.UnitOfWorks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Role.UpdateRoleByRoleId;

/// <summary>
///     Update role by role id request handler.
/// </summary>
internal sealed class UpdateRoleByRoleIdRequestHandler : IRequestHandler<
    UpdateRoleByRoleIdRequest,
    UpdateRoleByRoleIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public UpdateRoleByRoleIdRequestHandler(
        IUnitOfWork unitOfWork,
        ISuperSpecificationManager superSpecificationManager)
    {
        _unitOfWork = unitOfWork;
        _superSpecificationManager = superSpecificationManager;
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
    public async Task<UpdateRoleByRoleIdResponse> Handle(
        UpdateRoleByRoleIdRequest request,
        CancellationToken cancellationToken)
    {
        // Is role found by role id.
        var isRoleFoundByRoleId = await IsRoleFoundByRoleIdQueryAsync(
            roleId: request.RoleId,
            cancellationToken: cancellationToken);

        // Role is not found by role id.
        if (!isRoleFoundByRoleId)
        {
            return new()
            {
                StatusCode = UpdateRoleByRoleIdResponseStatusCode.DEPARTMENT_IS_NOT_FOUND
            };
        }

        // Is role temporarily removed by role id.
        var isRoleTemporarilyRemoved = await IsRoleTemporarilyRemovedByRoleIdQueryAsync(
            roleId: request.RoleId,
            cancellationToken: cancellationToken);

        // Role is already temporarily removed by role id.
        if (isRoleTemporarilyRemoved)
        {
            return new()
            {
                StatusCode = UpdateRoleByRoleIdResponseStatusCode.DEPARTMENT_IS_ALREADY_TEMPORARILY_REMOVED
            };
        }

        // Is role with the same role name found.
        var isRoleWithTheSameNameFound = await IsRoleWithTheSameNameFoundByRoleNameQueryAsync(
            newRoleName: request.NewRoleName,
            cancellationToken: cancellationToken);

        // Role with the same role name is found.
        if (isRoleWithTheSameNameFound)
        {
            return new()
            {
                StatusCode = UpdateRoleByRoleIdResponseStatusCode.DEPARTMENT_ALREADY_EXISTS
            };
        }

        // Update role by role id.
        var result = await UpdateRoleByRoleIdCommandAsync(
            request: request,
            cancellationToken: cancellationToken);

        // Database transaction false.
        if (!result)
        {
            return new()
            {
                StatusCode = UpdateRoleByRoleIdResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        return new()
        {
            StatusCode = UpdateRoleByRoleIdResponseStatusCode.OPERATION_SUCCESS
        };
    }

    #region Queries
    /// <summary>
    ///     Is role found by role id.
    /// </summary>
    /// <param name="roleId">
    ///     Role id.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     True if role is found by role
    ///     id. Otherwise, false.
    /// </returns>
    private Task<bool> IsRoleFoundByRoleIdQueryAsync(
        Guid roleId,
        CancellationToken cancellationToken)
    {
        return _unitOfWork.RoleRepository.IsFoundBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Role.RoleByIdSpecification(roleId: roleId),
            ],
            cancellationToken: cancellationToken);
    }

    /// <summary>
    ///     Is role temporarily removed by role id.
    /// </summary>
    /// <param name="roleId">
    ///     Role id.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     True if role is temporarily removed by role id.
    ///     Otherwise, false.
    /// </returns>
    private Task<bool> IsRoleTemporarilyRemovedByRoleIdQueryAsync(
        Guid roleId,
        CancellationToken cancellationToken)
    {
        return _unitOfWork.RoleRepository.IsFoundBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Role.RoleByIdSpecification(roleId: roleId),
                _superSpecificationManager.Role.RoleTemporarilyRemovedSpecification
            ],
            cancellationToken: cancellationToken);
    }

    /// <summary>
    ///     Is role having the same name with
    ///     the new one found.
    /// </summary>
    /// <param name="newRoleName">
    ///     New role name.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     True if role already exists. Otherwise, false.
    /// </returns>
    private Task<bool> IsRoleWithTheSameNameFoundByRoleNameQueryAsync(
        string newRoleName,
        CancellationToken cancellationToken)
    {
        return _unitOfWork.RoleRepository.IsFoundBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Role.RoleByNameSpecification(
                    roleName: newRoleName,
                    isCaseSensitive: true),
            ],
            cancellationToken: cancellationToken);
    }
    #endregion

    #region Commands
    /// <summary>
    ///     Attempt to update role with new name by role id.
    /// </summary>
    /// <param name="request">
    ///     Containing role information.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     True if updating role operation is successful.
    ///     Otherwise, false.
    /// </returns>
    private async Task<bool> UpdateRoleByRoleIdCommandAsync(
        UpdateRoleByRoleIdRequest request,
        CancellationToken cancellationToken)
    {
        var executedTransactionResult = false;

        await _unitOfWork
            .CreateExecutionStrategy()
            .ExecuteAsync(operation: async () =>
            {
                try
                {
                    await _unitOfWork.CreateTransactionAsync(cancellationToken: cancellationToken);

                    /*var foundUsers = await _unitOfWork.UserRepository.GetAllBySpecificationsAsync(
                        specifications:
                        [
                            _superSpecificationManager.User.UserAsNoTrackingSpecification,
                            _superSpecificationManager.User.UserByRoleIdSpecification(
                                roleId: request.RoleId),
                            _superSpecificationManager.User.SelectFieldsFromUserSpecification.Ver5(),
                        ],
                        cancellationToken: cancellationToken);

                    foreach (var foundUser in foundUsers)
                    {
                        await _unitOfWork.UserRepository.BulkUpdateByUserIdVer1Async(
                            userId: foundUser.Id,
                            userUpdatedAt: DateTime.UtcNow,
                            userUpdatedBy: request.RoleUpdatedBy,
                            cancellationToken: cancellationToken);
                    }*/

                    await _unitOfWork.RoleRepository.BulkUpdateByRoleIdVer2Async(
                        roleId: request.RoleId,
                        roleName: request.NewRoleName,
                        cancellationToken: cancellationToken);

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
