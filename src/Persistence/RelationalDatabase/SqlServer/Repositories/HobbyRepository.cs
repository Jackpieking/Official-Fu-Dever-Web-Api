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
            hobbyName.Length > Skill.Metadata.Name.MaxLength)
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