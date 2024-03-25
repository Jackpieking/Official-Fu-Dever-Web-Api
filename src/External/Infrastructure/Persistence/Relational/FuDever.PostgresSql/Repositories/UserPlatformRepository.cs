using FuDever.Domain.Entities;
using FuDever.Domain.Repositories;
using FuDever.PostgresSql.Data;
using FuDever.PostgresSql.Repositories.Base;

namespace FuDever.PostgresSql.Repositories;

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
