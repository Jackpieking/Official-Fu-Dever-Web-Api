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
///     Implementation of hobby repository.
/// </summary>
internal sealed class HobbyRepository :
    BaseRepository<Hobby>,
    IHobbyRepository
{
    internal HobbyRepository(FuDeverContext context) : base(context: context)
    {
    }

    public Task<int> BulkUpdateByHobbyIdVer1Async(
        Guid hobbyId,
        string hobbyName,
        CancellationToken cancellationToken)
    {
        if (hobbyId == Guid.Empty ||
            string.IsNullOrWhiteSpace(value: hobbyName) ||
            hobbyName.Length > Hobby.Metadata.Name.MaxLength ||
            hobbyName.Length < Hobby.Metadata.Name.MinLength)
        {
            return Task.FromResult<int>(result: default);
        }

        return _dbSet
            .Where(predicate: hobby => hobby.Id == hobbyId)
            .ExecuteUpdateAsync(
                setPropertyCalls: setter => setter
                    .SetProperty(
                        hobby => hobby.Name,
                        hobbyName),
                cancellationToken: cancellationToken);
    }

    public Task<int> BulkUpdateByHobbyIdVer2Async(
        Guid hobbyId,
        DateTime hobbyRemovedAt,
        Guid hobbyRemovedBy,
        CancellationToken cancellationToken)
    {
        if (hobbyId == Guid.Empty ||
            hobbyRemovedAt < CommonConstant.DbDefaultValue.MIN_DATE_TIME ||
            hobbyRemovedAt > DateTime.UtcNow ||
            hobbyRemovedBy == Guid.Empty)
        {
            return Task.FromResult<int>(result: default);
        }

        return _dbSet
            .Where(predicate: hobby => hobby.Id == hobbyId)
            .ExecuteUpdateAsync(
                setPropertyCalls: setter => setter
                    .SetProperty(
                        hobby => hobby.RemovedAt,
                        hobbyRemovedAt)
                    .SetProperty(
                        hobby => hobby.RemovedBy,
                        hobbyRemovedBy),
                cancellationToken: cancellationToken);
    }

    public Task<int> BulkRemoveByHobbyIdAsync(
        Guid hobbyId,
        CancellationToken cancellationToken)
    {
        if (hobbyId == Guid.Empty)
        {
            return Task.FromResult<int>(result: default);
        }

        return _dbSet
            .Where(predicate: hobby => hobby.Id == hobbyId)
            .ExecuteDeleteAsync(cancellationToken: cancellationToken);
    }
}