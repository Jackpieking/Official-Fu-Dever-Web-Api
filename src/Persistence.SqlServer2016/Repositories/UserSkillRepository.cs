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
///     Implementation of user skill repository.
/// </summary>
internal sealed class UserSkillRepository :
    BaseRepository<UserSkill>,
    IUserSkillRepository
{
    internal UserSkillRepository(IFuDeverContext context) : base(context: context)
    {
    }

    public Task<int> BulkRemoveBySkillIdAsync(
        Guid skillId,
        CancellationToken cancellationToken)
    {
        return _dbSet
            .Where(predicate: userSkill => userSkill.SkillId == skillId)
            .ExecuteDeleteAsync(cancellationToken: cancellationToken);
    }

    public Task<int> BulkRemoveByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        return _dbSet
            .Where(predicate: userSkill => userSkill.UserId == userId)
            .ExecuteDeleteAsync(cancellationToken: cancellationToken);
    }
}

