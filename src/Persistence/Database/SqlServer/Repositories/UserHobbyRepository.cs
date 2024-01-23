using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Database.SqlServer.Data;
using Persistence.Database.SqlServer.Repositories.Base;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence.Database.SqlServer.Repositories;

/// <summary>
///     Implementation of user hobby repository.
/// </summary>
internal sealed class UserHobbyRepository :
    BaseRepository<UserHobby>,
    IUserHobbyRepository
{
    internal UserHobbyRepository(FuDeverContext context) : base(context: context)
    {
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
            .Where(userHobby => userHobby.HobbyId == hobbyId)
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
            .Where(userHobby => userHobby.UserId == userId)
            .ExecuteDeleteAsync(cancellationToken: cancellationToken);
    }
}
