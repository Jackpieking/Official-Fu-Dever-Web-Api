using FuDever.Domain.Entities;
using FuDever.Domain.Repositories;
using FuDever.PostgresSql.Data;
using FuDever.PostgresSql.Repositories.Base;

namespace FuDever.PostgresSql.Repositories;

/// <summary>
///     Implementation of user joining status repository.
/// </summary>
internal sealed class UserJoiningStatusRepository :
    BaseRepository<UserJoiningStatus>,
    IUserJoiningStatusRepository
{
    internal UserJoiningStatusRepository(FuDeverContext context) : base(context: context)
    {
    }
}
