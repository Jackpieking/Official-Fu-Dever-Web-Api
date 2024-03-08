using Domain.Entities;
using Domain.Repositories;
using Persistence.RelationalDatabase.SqlServer.Data;
using Persistence.RelationalDatabase.SqlServer.Repositories.Base;

namespace Persistence.RelationalDatabase.SqlServer.Repositories;

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
