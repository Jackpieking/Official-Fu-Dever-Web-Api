using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.SqlServer2016.Data;
using Persistence.SqlServer2016.Repositories.Base;

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
        return _dbSet
            .Where(predicate: platform => platform.Id == platformId)
            .ExecuteDeleteAsync(cancellationToken: cancellationToken);
    }

    public Task<int> BulkUpdateByPlatformIdAsync(
        Guid platformId,
        DateTime platformRemovedAt,
        Guid platformRemovedBy,
        CancellationToken cancellationToken)
    {
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

    public Task<int> BulkUpdateByPlatformIdAsync(
        Guid platformId,
        string platformName,
        CancellationToken cancellationToken)
    {
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
