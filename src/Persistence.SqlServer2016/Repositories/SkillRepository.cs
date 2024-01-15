using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Data;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.SqlServer2016.Data;
using Persistence.SqlServer2016.Repositories.Base;

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
        return _dbSet
            .Where(predicate: skill => skill.Id == skillId)
            .ExecuteDeleteAsync(cancellationToken: cancellationToken);
    }

    public Task<int> BulkUpdateBySkillIdAsync(
        Guid skillId,
        DateTime skillRemovedAt,
        Guid skillRemovedBy,
        CancellationToken cancellationToken)
    {
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

    public Task<int> BulkUpdateBySkillIdAsync(
        Guid skillId,
        string skillName,
        CancellationToken cancellationToken)
    {
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
