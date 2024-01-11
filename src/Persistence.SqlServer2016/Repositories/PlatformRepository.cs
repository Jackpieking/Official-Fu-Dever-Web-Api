using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Data;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.SqlServer2016.Repositories.Base;

namespace Persistence.SqlServer2016.Repositories;

/// <summary>
///     Implementation of platform repository.
/// </summary>
internal sealed class PlatformRepository :
    BaseRepository<Platform>,
    IPlatformRepository
{
    internal PlatformRepository(IFuDeverContext context) : base(context: context)
    {
    }

    public Task<int> BulkRemoveByIdAsync(
        Guid platformId,
        CancellationToken cancellationToken)
    {
        return _dbSet
            .Where(predicate: platform => platform.Id == platformId)
            .ExecuteDeleteAsync(cancellationToken: cancellationToken);
    }

    public Task<int> BulkUpdateByIdVer1Async(
        Platform foundPlatform,
        CancellationToken cancellationToken)
    {
        return _dbSet
            .Where(predicate: platform => platform.Id == foundPlatform.Id)
            .ExecuteUpdateAsync(
                setPropertyCalls: setter => setter
                    .SetProperty(
                        platform => platform.RemovedAt,
                        foundPlatform.RemovedAt)
                    .SetProperty(
                        platform => platform.RemovedBy,
                        foundPlatform.RemovedBy),
                cancellationToken: cancellationToken);
    }

    public Task<int> BulkUpdateByIdVer2Async(
        Platform foundPlatform,
        CancellationToken cancellationToken)
    {
        return _dbSet
            .Where(predicate: platform => platform.Id == foundPlatform.Id)
            .ExecuteUpdateAsync(
                setPropertyCalls: setter => setter
                    .SetProperty(
                        platform => platform.Name,
                        foundPlatform.Name),
                cancellationToken: cancellationToken);
    }
}
