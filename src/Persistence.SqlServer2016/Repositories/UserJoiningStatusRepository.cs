using Domain.Data;
using Domain.Entities;
using Domain.Repositories;
using Persistence.SqlServer2016.Repositories.Base;

namespace Persistence.SqlServer2016.Repositories;

/// <summary>
///     Implementation of user joining status repository.
/// </summary>
internal sealed class UserJoiningStatusRepository :
    BaseRepository<UserJoiningStatus>,
    IUserJoiningStatusRepository
{
    internal UserJoiningStatusRepository(IFuDeverContext context) : base(context: context)
    {
    }
}
