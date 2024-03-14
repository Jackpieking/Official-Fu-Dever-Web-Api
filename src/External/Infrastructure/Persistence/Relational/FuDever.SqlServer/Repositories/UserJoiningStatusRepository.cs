using FuDever.Domain.Entities;
using FuDever.Domain.Repositories;
using FuDever.SqlServer.Data;
using FuDever.SqlServer.Repositories.Base;

namespace FuDever.SqlServer.Repositories;

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
