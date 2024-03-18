using FuDever.Domain.Entities;
using FuDever.Domain.Repositories;
using FuDever.PostgresSql.Data;
using FuDever.PostgresSql.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.PostgresSql.Repositories;

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
