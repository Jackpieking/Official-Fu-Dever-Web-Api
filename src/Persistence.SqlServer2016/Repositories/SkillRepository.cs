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
///     Implementation of skill repository.
/// </summary>
internal sealed class SkillRepository :
    BaseRepository<Skill>,
    ISkillRepository
{
    internal SkillRepository(IFuDeverContext context) : base(context: context)
    {
    }

    public Task<int> BulkRemoveByIdAsync(
        Guid skillId,
        CancellationToken cancellationToken)
    {
        return _dbSet
            .Where(predicate: skill => skill.Id == skillId)
            .ExecuteDeleteAsync(cancellationToken: cancellationToken);
    }

    public Task<int> BulkUpdateByIdVer1Async(
        Skill foundSkill,
        CancellationToken cancellationToken)
    {
        return _dbSet
            .Where(predicate: skill => skill.Id == foundSkill.Id)
            .ExecuteUpdateAsync(
                setPropertyCalls: setter => setter
                    .SetProperty(
                        skill => skill.RemovedAt,
                        foundSkill.RemovedAt)
                    .SetProperty(
                        skill => skill.RemovedBy,
                        foundSkill.RemovedBy),
                cancellationToken: cancellationToken);
    }

    public Task<int> BulkUpdateByIdVer2Async(
        Skill foundSkill,
        CancellationToken cancellationToken)
    {
        return _dbSet
            .Where(predicate: skill => skill.Id == foundSkill.Id)
            .ExecuteUpdateAsync(
                setPropertyCalls: setter => setter
                    .SetProperty(
                        skill => skill.Name,
                        foundSkill.Name),
                cancellationToken: cancellationToken);
    }
}
