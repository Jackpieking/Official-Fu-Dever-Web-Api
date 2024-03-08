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
///     Implementation of position repository.
/// </summary>
internal sealed class PositionRepository :
    BaseRepository<Position>,
    IPositionRepository
{
    internal PositionRepository(FuDeverContext context) : base(context: context)
    {
    }

    public Task<int> BulkRemoveByPositionIdAsync(
        Guid positionId,
        CancellationToken cancellationToken)
    {
        if (positionId == Guid.Empty)
        {
            return Task.FromResult<int>(result: default);
        }

        return _dbSet
            .Where(predicate: position => position.Id == positionId)
            .ExecuteDeleteAsync(cancellationToken: cancellationToken);
    }

    public Task<int> BulkUpdateByPositionIdVer1Async(
        Guid positionId,
        DateTime positionRemovedAt,
        Guid positionRemovedBy,
        CancellationToken cancellationToken)
    {
        if (positionId == Guid.Empty ||
            positionRemovedAt < CommonConstant.DbDefaultValue.MIN_DATE_TIME ||
            positionRemovedAt > DateTime.UtcNow ||
            positionRemovedBy == Guid.Empty)
        {
            return Task.FromResult<int>(result: default);
        }

        return _dbSet
            .Where(predicate: position => position.Id == positionId)
            .ExecuteUpdateAsync(
                setPropertyCalls: setter => setter
                    .SetProperty(
                        position => position.RemovedAt,
                        positionRemovedAt)
                    .SetProperty(
                        position => position.RemovedBy,
                        positionRemovedBy),
                cancellationToken: cancellationToken);
    }

    public Task<int> BulkUpdateByPositionIdVer2Async(
        Guid positionId,
        string positionName,
        CancellationToken cancellationToken)
    {
        if (positionId == Guid.Empty ||
            string.IsNullOrWhiteSpace(value: positionName) ||
            positionName.Length > Position.Metadata.Name.MaxLength)
        {
            return Task.FromResult<int>(result: default);
        }

        return _dbSet
            .Where(predicate: position => position.Id == positionId)
            .ExecuteUpdateAsync(
                setPropertyCalls: setter => setter
                    .SetProperty(
                        position => position.Name,
                        positionName),
                cancellationToken: cancellationToken);
    }
}