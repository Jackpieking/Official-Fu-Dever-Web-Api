using FuDever.Domain.Entities;
using FuDever.Domain.Repositories;
using FuDever.PostgresSql.Data;
using FuDever.PostgresSql.Repositories.Base;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;
using Microsoft.EntityFrameworkCore;

namespace FuDever.PostgresSql.Repositories;

/// <summary>
///     Implementation of user role repository.
/// </summary>
internal sealed class UserRoleRepository :
    BaseRepository<UserRole>,
    IUserRoleRepository
{
    public UserRoleRepository(FuDeverContext context) : base(context: context)
    {
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
            .Where(predicate: userRole => userRole.RoleId == roleId)
            .ExecuteDeleteAsync(cancellationToken: cancellationToken);
    }
}
