using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.SqlServer2016.Common;
using Persistence.SqlServer2016.Data;
using Persistence.SqlServer2016.Repositories.Base;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence.SqlServer2016.Repositories;

/// <summary>
///     Implementation of platform repository.
/// </summary>
internal sealed class PlatformRepository :
    BaseRepository<Platform>,
    IPlatformRepository
{
    internal PlatformRepository(FuDeverContext context) : base(context: context)
    {
    }

    public Task<int> BulkRemoveByPlatformIdAsync(
        Guid platformId,
        CancellationToken cancellationToken)
    {
        if (platformId == Guid.Empty)
        {
            return Task.FromResult<int>(result: default);
        }

        return _dbSet
            .Where(predicate: platform => platform.Id == platformId)
            .ExecuteDeleteAsync(cancellationToken: cancellationToken);
    }

    public Task<int> BulkUpdateByPlatformIdVer1Async(
        Guid platformId,
        DateTime platformRemovedAt,
        Guid platformRemovedBy,
        CancellationToken cancellationToken)
    {
        if (platformId == Guid.Empty ||
            platformRemovedAt < CommonConstant.DbDefaultValue.MIN_DATE_TIME ||
            platformRemovedAt > DateTime.UtcNow ||
            platformRemovedBy == Guid.Empty)
        {
            return Task.FromResult<int>(result: default);
        }

        return _dbSet
            .Where(predicate: platform => platform.Id == platformId)
            .ExecuteUpdateAsync(
                setPropertyCalls: setter => setter
                    .SetProperty(
                        platform => platform.RemovedAt,
                        platformRemovedAt)
                    .SetProperty(
                        platform => platform.RemovedBy,
                        platformRemovedBy),
                cancellationToken: cancellationToken);
    }

    public Task<int> BulkUpdateByPlatformIdVer2Async(
        Guid platformId,
        string platformName,
        CancellationToken cancellationToken)
    {
        const int MaxPlatformNameLength = 100;

        if (platformId == Guid.Empty ||
            string.IsNullOrWhiteSpace(value: platformName) ||
            platformName.Length > MaxPlatformNameLength)
        {
            return Task.FromResult<int>(result: default);
        }

        return _dbSet
            .Where(predicate: platform => platform.Id == platformId)
            .ExecuteUpdateAsync(
                setPropertyCalls: setter => setter
                    .SetProperty(
                        platform => platform.Name,
                        platformName),
                cancellationToken: cancellationToken);
    }
}
