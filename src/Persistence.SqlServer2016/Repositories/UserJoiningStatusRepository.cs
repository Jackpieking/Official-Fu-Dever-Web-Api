using Domain.Entities;
using Domain.Repositories;
using Persistence.SqlServer2016.Data;
using Persistence.SqlServer2016.Repositories.Base;

namespace Persistence.SqlServer2016.Repositories;

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
