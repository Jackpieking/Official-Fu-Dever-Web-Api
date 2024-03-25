using FuDever.Application.Commons;
using FuDever.Application.Interfaces.Data;
using FuDever.Domain.Specifications.Others.Interfaces;
using FuDever.Domain.UnitOfWorks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Role.RestoreRoleByRoleId;

/// <summary>
///     Restore role by role id request handler.
/// </summary>
internal sealed class RestoreRoleByRoleIdRequestHandler : IRequestHandler<
    RestoreRoleByRoleIdRequest,
    RestoreRoleByRoleIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;
    private readonly IDbMinTimeHandler _dbMinTimeHandler;

    public RestoreRoleByRoleIdRequestHandler(
        IUnitOfWork unitOfWork,
        ISuperSpecificationManager superSpecificationManager,
        IDbMinTimeHandler dbMinTimeHandler)
    {
        _unitOfWork = unitOfWork;
        _superSpecificationManager = superSpecificationManager;
        _dbMinTimeHandler = dbMinTimeHandler;
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
    public async Task<RestoreRoleByRoleIdResponse> Handle(
        RestoreRoleByRoleIdRequest request,
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
                StatusCode = RestoreRoleByRoleIdResponseStatusCode.ROLE_IS_NOT_FOUND
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
                StatusCode = RestoreRoleByRoleIdResponseStatusCode.ROLE_IS_NOT_TEMPORARILY_REMOVED
            };
        }

        // Remove role permanently by role id.
        var result = await RestoreRoleByRoleIdCommandAsync(
            request: request,
            cancellationToken: cancellationToken);

        // Database transaction false.
        if (!result)
        {
            return new()
            {
                StatusCode = RestoreRoleByRoleIdResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        return new()
        {
            StatusCode = RestoreRoleByRoleIdResponseStatusCode.OPERATION_SUCCESS
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
    ///     Attempt to restore role by role id.
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
    ///     True if restoring skill permanently operation is
    ///     successful. Otherwise, false.
    /// </returns>
    public async Task<bool> RestoreRoleByRoleIdCommandAsync(
        RestoreRoleByRoleIdRequest request,
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
                                roleRemovedAt: _dbMinTimeHandler.Get(),
                                roleRemovedBy: CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID)
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
