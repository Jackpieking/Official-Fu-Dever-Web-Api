using FuDever.Domain.Entities;
using FuDever.Domain.Repositories;
using FuDever.SqlServer.Commons;
using FuDever.SqlServer.Data;
using FuDever.SqlServer.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.SqlServer.Repositories;

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