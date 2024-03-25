using FuDever.Domain.Specifications.Others.Interfaces;
using FuDever.Domain.UnitOfWorks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Role.RemoveRoleTemporarilyByRoleId;

/// <summary>
///     Remove role temporarily by role id request handler.
/// </summary>
internal sealed class RemoveRoleTemporarilyByRoleIdRequestHandler : IRequestHandler<
    RemoveRoleTemporarilyByRoleIdRequest,
    RemoveRoleTemporarilyByRoleIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public RemoveRoleTemporarilyByRoleIdRequestHandler(
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
    public async Task<RemoveRoleTemporarilyByRoleIdResponse> Handle(
        RemoveRoleTemporarilyByRoleIdRequest request,
        CancellationToken cancellationToken)
    {
        // Is role found by role id.
        var isRoleFound = await IsRoleFoundByRoleIdQueryAsync(
            roleId: request.RoleId,
            cancellationToken: cancellationToken);

        // Role is not found by role id.
        if (!isRoleFound)
        {
            return new()
            {
                StatusCode = RemoveRoleTemporarilyByRoleIdResponseStatusCode.ROLE_IS_NOT_FOUND
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
                StatusCode = RemoveRoleTemporarilyByRoleIdResponseStatusCode.ROLE_IS_ALREADY_TEMPORARILY_REMOVED
            };
        }

        // Remove role temporarily by role id.
        var result = await RemoveRoleTemporarilyByRoleIdCommandAsync(
            request: request,
            cancellationToken: cancellationToken);

        // Database transaction false.
        if (!result)
        {
            return new()
            {
                StatusCode = RemoveRoleTemporarilyByRoleIdResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        return new()
        {
            StatusCode = RemoveRoleTemporarilyByRoleIdResponseStatusCode.OPERATION_SUCCESS
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
    ///     Attempt to remove this role temporarily.
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
    ///     True if removed successfully. Otherwise, false.
    /// </returns>
    private async Task<bool> RemoveRoleTemporarilyByRoleIdCommandAsync(
        RemoveRoleTemporarilyByRoleIdRequest request,
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

                    await _unitOfWork.RoleRepository.BulkUpdateAsync(
                        specifications:
                        [
                            _superSpecificationManager.Role.RoleByIdSpecification(
                                roleId: request.RoleId),
                            _superSpecificationManager.Role.UpdateFieldOfRoleSpecification.Ver1(
                                roleRemovedAt: DateTime.UtcNow,
                                roleRemovedBy: request.RoleRemovedBy)
                        ],
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
