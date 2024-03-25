using FuDever.Domain.Entities;
using FuDever.Domain.Repositories;
using FuDever.SqlServer.Data;
using FuDever.SqlServer.Repositories.Base;

namespace FuDever.SqlServer.Repositories;

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
