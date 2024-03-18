using FuDever.Domain.Entities;
using FuDever.Domain.Repositories;
using FuDever.PostgresSql.Commons;
using FuDever.PostgresSql.Data;
using FuDever.PostgresSql.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.PostgresSql.Repositories;

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
        Guid userPositionId,
        CancellationToken cancellationToken)
    {
        if (userId == Guid.Empty ||
            userUpdatedAt < CommonConstant.DbDefaultValue.MIN_DATE_TIME ||
            userUpdatedAt > DateTime.UtcNow ||
            userUpdatedBy == Guid.Empty ||
            userPositionId == Guid.Empty)
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
                        userPositionId),
                cancellationToken: cancellationToken);
    }

    public Task<int> BulkUpdateByUserIdVer3Async(
        Guid userId,
        DateTime userUpdatedAt,
        Guid userUpdatedBy,
        Guid userDepartmentId,
        CancellationToken cancellationToken)
    {
        if (userId == Guid.Empty ||
            userUpdatedAt < CommonConstant.DbDefaultValue.MIN_DATE_TIME ||
            userUpdatedAt > DateTime.UtcNow ||
            userUpdatedBy == Guid.Empty ||
            userDepartmentId == Guid.Empty)
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
                        user => user.DepartmentId,
                        userDepartmentId),
                cancellationToken: cancellationToken);
    }
}
