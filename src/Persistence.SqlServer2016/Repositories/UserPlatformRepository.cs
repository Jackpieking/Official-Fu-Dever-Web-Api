using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.SqlServer2016.Data;
using Persistence.SqlServer2016.Repositories.Base;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence.SqlServer2016.Repositories;

/// <summary>
///     Implementation of user platform repository.
/// </summary>
internal sealed class UserPlatformRepository :
    BaseRepository<UserPlatform>,
    IUserPlatformRepository
{
    internal UserPlatformRepository(FuDeverContext context) : base(context: context)
    {
    }

    public Task<int> BulkRemoveByPlatformIdAsync(
        Guid platformId,
        CancellationToken cancellationToken)
    {
        if (platformId == Guid.Empty)
        {
            return Task.FromResult<int>(result: default);
        }

        return _dbSet
            .Where(userPlatform => userPlatform.PlatformId == platformId)
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
            .Where(userPlatform => userPlatform.UserId == userId)
            .ExecuteDeleteAsync(cancellationToken: cancellationToken);
    }
}
