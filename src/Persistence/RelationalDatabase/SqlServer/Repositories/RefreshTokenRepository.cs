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
        Guid refreshTokenCreatedBy,
        CancellationToken cancellationToken)
    {
        if (refreshTokenCreatedBy == Guid.Empty)
        {
            return Task.FromResult<int>(result: default);
        }

        return _dbSet
            .Where(refreshToken => refreshToken.CreatedBy == refreshTokenCreatedBy)
            .ExecuteDeleteAsync(cancellationToken: cancellationToken);
    }
}