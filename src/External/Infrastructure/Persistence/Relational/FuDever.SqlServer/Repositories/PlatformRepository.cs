using FuDever.Domain.Entities;
using FuDever.Domain.Repositories;
using FuDever.SqlServer.Data;
using FuDever.SqlServer.Repositories.Base;

namespace FuDever.SqlServer.Repositories;

/// <summary>
///     Implementation of platform repository.
/// </summary>
internal sealed class PlatformRepository :
    BaseRepository<Platform>,
    IPlatformRepository
{
    internal PlatformRepository(FuDeverContext context) : base(context: context)
    {
    }
}
