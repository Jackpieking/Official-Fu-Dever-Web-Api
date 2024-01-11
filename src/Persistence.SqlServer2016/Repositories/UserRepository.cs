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
///     Implementation of user repository.
/// </summary>
internal sealed class UserRepository :
    BaseRepository<User>,
    IUserRepository
{
    internal UserRepository(IFuDeverContext context) : base(context: context)
    {
    }

    public Task<int> BulkUpdateByIdVer1Async(
        User foundUser,
        CancellationToken cancellationToken)
    {
        return _dbSet
            .Where(predicate: user => user.Id == foundUser.Id)
            .ExecuteUpdateAsync(
                setPropertyCalls: setter => setter
                    .SetProperty(
                        user => user.UpdatedAt,
                        foundUser.UpdatedAt)
                    .SetProperty(
                        user => user.UpdatedBy,
                        foundUser.UpdatedBy),
                cancellationToken: cancellationToken);
    }

    public Task<int> BulkUpdateByIdVer2Async(
        User foundUser,
        CancellationToken cancellationToken)
    {
        return _dbSet
            .Where(predicate: user => user.Id == foundUser.Id)
            .ExecuteUpdateAsync(
                setPropertyCalls: setter => setter
                    .SetProperty(
                        user => user.UpdatedAt,
                        foundUser.UpdatedAt)
                    .SetProperty(
                        user => user.UpdatedBy,
                        foundUser.UpdatedBy)
                    .SetProperty(
                        user => user.PositionId,
                        foundUser.PositionId),
                cancellationToken: cancellationToken);
    }
}

