using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.SqlServer2016.Data;
using Persistence.SqlServer2016.Repositories.Base;

namespace Persistence.SqlServer2016.Repositories;

/// <summary>
///     Implementation of refresh token repository.
/// </summary>
internal sealed class RefreshTokenRepository :
    BaseRepository<RefreshToken>,
    IRefreshTokenRepository
{
    internal RefreshTokenRepository(FuDeverContext context) : base(context: context)
    {
    }

    public Task BulkRemoveByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        return _dbSet
            .Where(refreshToken => refreshToken.UserId == userId)
            .ExecuteDeleteAsync(cancellationToken: cancellationToken);
    }
}