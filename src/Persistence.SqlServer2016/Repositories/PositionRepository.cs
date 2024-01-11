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
///     Implementation of position repository.
/// </summary>
internal sealed class PositionRepository :
    BaseRepository<Position>,
    IPositionRepository
{
    internal PositionRepository(IFuDeverContext context) : base(context: context)
    {
    }

    public Task<int> BulkRemoveByIdAsync(
        Guid positionId,
        CancellationToken cancellationToken)
    {
        return _dbSet
            .Where(predicate: position => position.Id == positionId)
            .ExecuteDeleteAsync(cancellationToken: cancellationToken);
    }

    public Task<int> BulkUpdateByIdVer1Async(
        Position foundPosition,
        CancellationToken cancellationToken)
    {
        return _dbSet
            .Where(predicate: position => position.Id == foundPosition.Id)
            .ExecuteUpdateAsync(
                setPropertyCalls: setter => setter
                    .SetProperty(
                        position => position.RemovedAt,
                        foundPosition.RemovedAt)
                    .SetProperty(
                        position => position.RemovedBy,
                        foundPosition.RemovedBy),
                cancellationToken: cancellationToken);
    }

    public Task<int> BulkUpdateByIdVer2Async(
        Position foundPosition,
        CancellationToken cancellationToken)
    {
        return _dbSet
            .Where(predicate: position => position.Id == foundPosition.Id)
            .ExecuteUpdateAsync(
                setPropertyCalls: setter => setter
                    .SetProperty(
                        position => position.Name,
                        foundPosition.Name),
                cancellationToken: cancellationToken);
    }
}
