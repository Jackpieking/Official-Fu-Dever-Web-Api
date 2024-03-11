using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.RelationalDatabase.SqlServer.Commons;
using Persistence.RelationalDatabase.SqlServer.Data;
using Persistence.RelationalDatabase.SqlServer.Repositories.Base;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence.RelationalDatabase.SqlServer.Repositories;

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
        if (platformId == Guid.Empty ||
            string.IsNullOrWhiteSpace(value: platformName) ||
            platformName.Length > Platform.Metadata.Name.MaxLength ||
            platformName.Length < Platform.Metadata.Name.MinLength)
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
