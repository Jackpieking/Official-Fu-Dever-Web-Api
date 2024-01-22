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
///     Implementation of skill repository.
/// </summary>
internal sealed class SkillRepository :
    BaseRepository<Skill>,
    ISkillRepository
{
    internal SkillRepository(FuDeverContext context) : base(context: context)
    {
    }

    public Task<int> BulkRemoveBySkillIdAsync(
        Guid skillId,
        CancellationToken cancellationToken)
    {
        if (skillId == Guid.Empty)
        {
            return Task.FromResult<int>(result: default);
        }

        return _dbSet
            .Where(predicate: skill => skill.Id == skillId)
            .ExecuteDeleteAsync(cancellationToken: cancellationToken);
    }

    public Task<int> BulkUpdateBySkillIdVer1Async(
        Guid skillId,
        DateTime skillRemovedAt,
        Guid skillRemovedBy,
        CancellationToken cancellationToken)
    {
        if (skillId == Guid.Empty ||
            skillRemovedAt < CommonConstant.DbDefaultValue.MIN_DATE_TIME ||
            skillRemovedAt > DateTime.UtcNow ||
            skillRemovedBy == Guid.Empty)
        {
            return Task.FromResult<int>(result: default);
        }

        return _dbSet
            .Where(predicate: skill => skill.Id == skillId)
            .ExecuteUpdateAsync(
                setPropertyCalls: setter => setter
                    .SetProperty(
                        skill => skill.RemovedAt,
                        skillRemovedAt)
                    .SetProperty(
                        skill => skill.RemovedBy,
                        skillRemovedBy),
                cancellationToken: cancellationToken);
    }

    public Task<int> BulkUpdateBySkillIdVer2Async(
        Guid skillId,
        string skillName,
        CancellationToken cancellationToken)
    {
        const int MaxSkillNameLength = 100;

        if (skillId == Guid.Empty ||
            string.IsNullOrWhiteSpace(value: skillName) ||
            skillName.Length > MaxSkillNameLength)
        {
            return Task.FromResult<int>(result: default);
        }

        return _dbSet
            .Where(predicate: skill => skill.Id == skillId)
            .ExecuteUpdateAsync(
                setPropertyCalls: setter => setter
                    .SetProperty(
                        skill => skill.Name,
                        skillName),
                cancellationToken: cancellationToken);
    }
}
