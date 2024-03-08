using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.RelationalDatabase.SqlServer.Data;
using Persistence.RelationalDatabase.SqlServer.Repositories.Base;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence.RelationalDatabase.SqlServer.Repositories;

/// <summary>
///     Implementation of user skill repository.
/// </summary>
internal sealed class UserSkillRepository :
    BaseRepository<UserSkill>,
    IUserSkillRepository
{
    internal UserSkillRepository(FuDeverContext context) : base(context: context)
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
            .Where(predicate: userSkill => userSkill.SkillId == skillId)
            .ExecuteDeleteAsync(cancellationToken: cancellationToken);
    }

    public Task<int> BulkRemoveByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        if (userId == Guid.Empty)
        {
            return Task.FromResult<int>(result: default);
        }

        return _dbSet
            .Where(predicate: userSkill => userSkill.UserId == userId)
            .ExecuteDeleteAsync(cancellationToken: cancellationToken);
    }
}

