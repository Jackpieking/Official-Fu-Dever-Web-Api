using Domain.Entities;
using Domain.Repositories;
using Persistence.RelationalDatabase.SqlServer.Data;
using Persistence.RelationalDatabase.SqlServer.Repositories.Base;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;
using Microsoft.EntityFrameworkCore;
using Persistence.RelationalDatabase.SqlServer.Commons;

namespace Persistence.RelationalDatabase.SqlServer.Repositories;

/// <summary>
///     Implementation of major repository.
/// </summary>
internal sealed class MajorRepository :
    BaseRepository<Major>,
    IMajorRepository
{
    internal MajorRepository(FuDeverContext context) : base(context: context)
    {
    }

    public Task<int> BulkUpdateByMajorIdVer1Async(
        Guid majorId,
        string majorName,
        CancellationToken cancellationToken)
    {
        if (majorId == Guid.Empty ||
            string.IsNullOrWhiteSpace(value: majorName) ||
            majorName.Length > Major.Metadata.Name.MaxLength ||
            majorName.Length < Major.Metadata.Name.MinLength)
        {
            return Task.FromResult<int>(result: default);
        }

        return _dbSet
            .Where(predicate: major => major.Id == majorId)
            .ExecuteUpdateAsync(
                setPropertyCalls: setter => setter
                    .SetProperty(
                        major => major.Name,
                        majorName),
                cancellationToken: cancellationToken);
    }

    public Task<int> BulkUpdateByMajorIdVer2Async(
        Guid majorId,
        DateTime majorRemovedAt,
        Guid majorRemovedBy,
        CancellationToken cancellationToken)
    {
        if (majorId == Guid.Empty ||
            majorRemovedAt < CommonConstant.DbDefaultValue.MIN_DATE_TIME ||
            majorRemovedAt > DateTime.UtcNow ||
            majorRemovedBy == Guid.Empty)
        {
            return Task.FromResult<int>(result: default);
        }

        return _dbSet
            .Where(predicate: major => major.Id == majorId)
            .ExecuteUpdateAsync(
                setPropertyCalls: setter => setter
                    .SetProperty(
                        major => major.RemovedAt,
                        majorRemovedAt)
                    .SetProperty(
                        major => major.RemovedBy,
                        majorRemovedBy),
                cancellationToken: cancellationToken);
    }
}