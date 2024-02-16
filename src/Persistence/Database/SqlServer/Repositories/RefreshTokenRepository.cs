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
        Guid createdBy,
        CancellationToken cancellationToken)
    {
        if (createdBy == Guid.Empty)
        {
            return Task.FromResult<int>(result: default);
        }

        return _dbSet
            .Where(refreshToken => refreshToken.CreatedBy == createdBy)
            .ExecuteDeleteAsync(cancellationToken: cancellationToken);
    }
}