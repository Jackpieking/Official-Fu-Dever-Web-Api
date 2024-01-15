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
        return _dbSet
            .Where(userHobby => userHobby.HobbyId == hobbyId)
            .ExecuteDeleteAsync(cancellationToken: cancellationToken);
    }

    public Task<int> BulkRemoveByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        return _dbSet
            .Where(userHobby => userHobby.UserId == userId)
            .ExecuteDeleteAsync(cancellationToken: cancellationToken);
    }
}
