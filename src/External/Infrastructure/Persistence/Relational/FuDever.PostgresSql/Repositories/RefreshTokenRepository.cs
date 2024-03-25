using FuDever.Domain.Entities;
using FuDever.Domain.Repositories;
using FuDever.PostgresSql.Data;
using FuDever.PostgresSql.Repositories.Base;

namespace FuDever.PostgresSql.Repositories;

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
}