using FuDever.Domain.Entities;
using FuDever.Domain.Repositories;
using FuDever.PostgresSql.Data;
using FuDever.PostgresSql.Repositories.Base;

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
}
