using FuDever.Domain.Entities;
using FuDever.Domain.Repositories;
using FuDever.SqlServer.Data;
using FuDever.SqlServer.Repositories.Base;

namespace FuDever.SqlServer.Repositories;

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
}
