using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Data;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.SqlServer2016.Data;
using Persistence.SqlServer2016.Repositories.Base;

namespace Persistence.SqlServer2016.Repositories;

/// <summary>
///     Implementation of user repository.
/// </summary>
internal sealed class UserRepository :
    BaseRepository<User>,
    IUserRepository
{
    internal UserRepository(FuDeverContext context) : base(context: context)
    {
    }

    public Task<int> BulkUpdateByUserIdAsync(
        Guid userId,
        DateTime userUpdatedAt,
        Guid userUpdatedBy,
        CancellationToken cancellationToken)
    {
        return _dbSet
            .Where(predicate: user => user.Id == userId)
            .ExecuteUpdateAsync(
                setPropertyCalls: setter => setter
                    .SetProperty(
                        user => user.UpdatedAt,
                        userUpdatedAt)
                    .SetProperty(
                        user => user.UpdatedBy,
                        userUpdatedBy),
                cancellationToken: cancellationToken);
    }

    public Task<int> BulkUpdateByUserIdAsync(
        Guid userId,
        DateTime userUpdatedAt,
        Guid userUpdatedBy,
        Guid positionId,
        CancellationToken cancellationToken)
    {
        return _dbSet
            .Where(predicate: user => user.Id == userId)
            .ExecuteUpdateAsync(
                setPropertyCalls: setter => setter
                    .SetProperty(
                        user => user.UpdatedAt,
                        userUpdatedAt)
                    .SetProperty(
                        user => user.UpdatedBy,
                        userUpdatedBy)
                    .SetProperty(
                        user => user.PositionId,
                        positionId),
                cancellationToken: cancellationToken);
    }
}

