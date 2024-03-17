using FuDever.Domain.Entities;
using FuDever.Domain.Repositories;
using FuDever.SqlServer.Commons;
using FuDever.SqlServer.Data;
using FuDever.SqlServer.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.SqlServer.Repositories;

/// <summary>
///     Implementation of role repository.
/// </summary>
internal sealed class RoleRepository :
    BaseRepository<Role>,
    IRoleRepository
{
    internal RoleRepository(FuDeverContext context) : base(context: context)
    {
    }

    public Task<int> BulkUpdateByRoleIdVer1Async(
        Guid roleId,
        DateTime roleRemovedAt,
        Guid roleRemovedBy,
        CancellationToken cancellationToken)
    {
        if (roleId == Guid.Empty ||
            roleRemovedAt < CommonConstant.DbDefaultValue.MIN_DATE_TIME ||
            roleRemovedAt > DateTime.UtcNow ||
            roleRemovedBy == Guid.Empty)
        {
            return Task.FromResult<int>(result: default);
        }

        return _dbSet
            .Where(predicate: role => role.Id == roleId)
            .ExecuteUpdateAsync(
                setPropertyCalls: setter => setter
                    .SetProperty(
                        role => role.RemovedAt,
                        roleRemovedAt)
                    .SetProperty(
                        role => role.RemovedBy,
                        roleRemovedBy),
                cancellationToken: cancellationToken);
    }

    public Task<int> BulkUpdateByRoleIdVer2Async(
        Guid roleId,
        string roleName,
        CancellationToken cancellationToken)
    {
        if (roleId == Guid.Empty ||
            string.IsNullOrWhiteSpace(value: roleName) ||
            roleName.Length > Role.Metadata.Name.MaxLength ||
            roleName.Length < Role.Metadata.Name.MinLength)
        {
            return Task.FromResult<int>(result: default);
        }

        return _dbSet
            .Where(predicate: role => role.Id == roleId)
            .ExecuteUpdateAsync(
                setPropertyCalls: setter => setter
                    .SetProperty(
                        role => role.Name,
                        roleName),
                cancellationToken: cancellationToken);
    }

    public Task<int> BulkRemoveByRoleIdAsync(
        Guid roleId,
        CancellationToken cancellationToken)
    {
        if (roleId == Guid.Empty)
        {
            return Task.FromResult<int>(result: default);
        }

        return _dbSet
            .Where(predicate: role => role.Id == roleId)
            .ExecuteDeleteAsync(cancellationToken: cancellationToken);
    }
}
