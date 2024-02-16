using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Commons;
using Persistence.Database.SqlServer.Data;
using Persistence.Database.SqlServer.Repositories.Base;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence.Database.SqlServer.Repositories;

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

    public Task<int> BulkUpdateByUserIdVer1Async(
        Guid userId,
        DateTime userUpdatedAt,
        Guid userUpdatedBy,
        CancellationToken cancellationToken)
    {
        if (userId == Guid.Empty ||
            userUpdatedAt < CommonConstant.DbDefaultValue.MIN_DATE_TIME ||
            userUpdatedAt > DateTime.UtcNow ||
            userUpdatedBy == Guid.Empty)
        {
            return Task.FromResult<int>(result: default);
        }

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

    public Task<int> BulkUpdateByUserIdVer2Async(
        Guid userId,
        DateTime userUpdatedAt,
        Guid userUpdatedBy,
        Guid positionId,
        CancellationToken cancellationToken)
    {
        if (userId == Guid.Empty ||
            userUpdatedAt < CommonConstant.DbDefaultValue.MIN_DATE_TIME ||
            userUpdatedAt > DateTime.UtcNow ||
            userUpdatedBy == Guid.Empty ||
            positionId == Guid.Empty)
        {
            return Task.FromResult<int>(result: default);
        }

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

    public Task<int> BulkUpdateByUserIdVer3Async(
        Guid userId,
        int accessFailedCount,
        DateTime lockoutEnd,
        CancellationToken cancellationToken)
    {
        if (userId == Guid.Empty ||
            accessFailedCount < default(int) ||
            lockoutEnd < DateTime.MinValue ||
            lockoutEnd > DateTime.MaxValue)
        {
            return Task.FromResult<int>(result: default);
        }

        return _dbSet
            .Where(predicate: user => user.Id == userId)
            .ExecuteUpdateAsync(
                setPropertyCalls: setter => setter
                    .SetProperty(
                        user => user.AccessFailedCount,
                        accessFailedCount)
                    .SetProperty(
                        user => user.LockoutEnd,
                        lockoutEnd),
                cancellationToken: cancellationToken);
    }

    public Task<int> BulkUpdateByUserIdVer4Async(
        Guid userId,
        int accessFailedCount,
        CancellationToken cancellationToken)
    {
        if (userId == Guid.Empty ||
            accessFailedCount < default(int))
        {
            return Task.FromResult<int>(result: default);
        }

        return _dbSet
            .Where(predicate: user => user.Id == userId)
            .ExecuteUpdateAsync(
                setPropertyCalls: setter => setter
                    .SetProperty(
                        user => user.AccessFailedCount,
                        accessFailedCount),
                cancellationToken: cancellationToken);
    }
}

