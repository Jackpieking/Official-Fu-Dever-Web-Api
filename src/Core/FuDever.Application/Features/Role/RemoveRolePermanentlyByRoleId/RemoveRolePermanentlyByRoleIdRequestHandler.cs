using FuDever.Domain.Specifications.Others.Interfaces;
using FuDever.Domain.UnitOfWorks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Role.RemoveRolePermanentlyByRoleId;

/// <summary>
///     Remove role permanently by role id request handler.
/// </summary>
internal sealed class RemoveRolePermanentlyByRoleIdRequestHandler : IRequestHandler<
    RemoveRolePermanentlyByRoleIdRequest,
    RemoveRolePermanentlyByRoleIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public RemoveRolePermanentlyByRoleIdRequestHandler(
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
    public async Task<RemoveRolePermanentlyByRoleIdResponse> Handle(
        RemoveRolePermanentlyByRoleIdRequest request,
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
                StatusCode = RemoveRolePermanentlyByRoleIdResponseStatusCode.ROLE_IS_NOT_FOUND
            };
        }

        // Is role temporarily removed by role id.
        var isRoleTemporarilyRemoved = await IsRoleTemporarilyRemovedByRoleIdQueryAsync(
            roleId: request.RoleId,
            cancellationToken: cancellationToken);

        // Role is not temporarily removed by role id.
        if (!isRoleTemporarilyRemoved)
        {
            return new()
            {
                StatusCode = RemoveRolePermanentlyByRoleIdResponseStatusCode.ROLE_IS_NOT_TEMPORARILY_REMOVED
            };
        }

        // Remove role permanently by role id.
        var result = await RemoveRolePermanentlyByRoleIdCommandAsync(
            request: request,
            cancellationToken: cancellationToken);

        // Database transaction false.
        if (!result)
        {
            return new()
            {
                StatusCode = RemoveRolePermanentlyByRoleIdResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        return new()
        {
            StatusCode = RemoveRolePermanentlyByRoleIdResponseStatusCode.OPERATION_SUCCESS
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
    #endregion

    #region Commands
    /// <summary>
    ///     Attempt to remove permanently role by role id.
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
    private async Task<bool> RemoveRolePermanentlyByRoleIdCommandAsync(
        RemoveRolePermanentlyByRoleIdRequest request,
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

                    var foundUserRoles = await _unitOfWork.UserRoleRepository.GetAllBySpecificationsAsync(
                        specifications:
                        [
                            _superSpecificationManager.UserRole.UserRoleAsNoTrackingSpecification,
                            _superSpecificationManager.UserRole.UserRoleByRoleIdSpecification(roleId: request.RoleId),
                            _superSpecificationManager.UserRole.SelectFieldsFromUserRoleSpecification.Ver1(),
                        ],
                        cancellationToken: cancellationToken);

                    foreach (var foundUserRole in foundUserRoles)
                    {
                        await _unitOfWork.UserRepository.BulkUpdateByUserIdVer1Async(
                            userId: foundUserRole.UserId,
                            userUpdatedAt: DateTime.UtcNow,
                            userUpdatedBy: request.RoleRemovedBy,
                            cancellationToken: cancellationToken);
                    }

                    await _unitOfWork.UserRoleRepository.BulkRemoveByRoleIdAsync(
                        roleId: request.RoleId,
                        cancellationToken: cancellationToken);

                    await _unitOfWork.RoleRepository.BulkRemoveByRoleIdAsync(
                        roleId: request.RoleId,
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
