using FuDever.Application.Commons;
using FuDever.Application.Interfaces.Data;
using FuDever.Domain.EntityBuilders.Role;
using FuDever.Domain.Specifications.Others.Interfaces;
using FuDever.Domain.UnitOfWorks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Role.CreateRole;

/// <summary>
///     Create role request handler.
/// </summary>
internal sealed class CreateRoleRequestHandler : IRequestHandler<
    CreateRoleRequest,
    CreateRoleResponse>
{
    private readonly RoleManager<Domain.Entities.Role> _roleManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;
    private readonly IDbMinTimeHandler _dbMinTimeHandler;

    public CreateRoleRequestHandler(
        IUnitOfWork unitOfWork,
        ISuperSpecificationManager superSpecificationManager,
        IDbMinTimeHandler dbMinTimeHandler,
        RoleManager<Domain.Entities.Role> roleManager)
    {
        _unitOfWork = unitOfWork;
        _superSpecificationManager = superSpecificationManager;
        _dbMinTimeHandler = dbMinTimeHandler;
        _roleManager = roleManager;
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
    public async Task<CreateRoleResponse> Handle(
        CreateRoleRequest request,
        CancellationToken cancellationToken)
    {
        // Is role with the same role name found.
        var isRoleFound = await IsRoleWithTheSameNameFoundByRoleNameQueryAsync(
            newRoleName: request.NewRoleName,
            cancellationToken: cancellationToken);

        // Roles with the same role name is found.
        if (isRoleFound)
        {
            // Is role temporarily removed by role name.
            var isRoleTemporarilyRemoved = await IsRoleTemporarilyRemovedByRoleNameQueryAsync(
                newRoleName: request.NewRoleName,
                cancellationToken: cancellationToken);

            // Role with role name is already temporarily removed.
            if (isRoleTemporarilyRemoved)
            {
                return new()
                {
                    StatusCode = CreateRoleResponseStatusCode.ROLE_IS_ALREADY_TEMPORARILY_REMOVED
                };
            }

            return new()
            {
                StatusCode = CreateRoleResponseStatusCode.ROLE_ALREADY_EXISTS
            };
        }

        // Create role with new role name.
        var result = await CreateRoleCommandAsync(
            request: request,
            cancellationToken: cancellationToken);

        // Database transaction false.
        if (!result)
        {
            return new()
            {
                StatusCode = CreateRoleResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        return new()
        {
            StatusCode = CreateRoleResponseStatusCode.OPERATION_SUCCESS
        };
    }

    #region Queries
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

    /// <summary>
    ///     Is role temporarily removed by role name.
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
    ///     True if role already temporarily removed. Otherwise, false.
    /// </returns>
    private Task<bool> IsRoleTemporarilyRemovedByRoleNameQueryAsync(
        string newRoleName,
        CancellationToken cancellationToken)
    {
        return _unitOfWork.RoleRepository.IsFoundBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Role.RoleByNameSpecification(
                    roleName: newRoleName,
                    isCaseSensitive: true),
                _superSpecificationManager.Role.RoleTemporarilyRemovedSpecification
            ],
            cancellationToken: cancellationToken);
    }
    #endregion

    #region Commands
    /// <summary>
    ///     Attempt to creating a new role with the
    ///     given name and add to database.
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
    ///     True if adding role operation is successful.
    ///     Otherwise, false.
    /// </returns>
    private async Task<bool> CreateRoleCommandAsync(
        CreateRoleRequest request,
        CancellationToken cancellationToken)
    {
        RoleForNewRecordBuilder builder = new();

        var newRole = builder
            .WithId(roleId: Guid.NewGuid())
            .WithName(roleName: request.NewRoleName)
            .WithRemovedAt(roleRemovedAt: _dbMinTimeHandler.Get())
            .WithRemovedBy(roleRemovedBy: CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID)
            .Complete();

        if (Equals(objA: newRole, objB: default))
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

                    await _roleManager.CreateAsync(role: newRole);

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
