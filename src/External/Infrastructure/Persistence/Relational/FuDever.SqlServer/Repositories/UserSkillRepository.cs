using FuDever.Domain.Entities;
using FuDever.Domain.Repositories;
using FuDever.SqlServer.Data;
using FuDever.SqlServer.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.SqlServer.Repositories;

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
