using Domain.Entities;
using Domain.Repositories;
using Persistence.RelationalDatabase.SqlServer.Data;
using Persistence.RelationalDatabase.SqlServer.Repositories.Base;

namespace Persistence.RelationalDatabase.SqlServer.Repositories;

/// <summary>
///     Implementation of role repository.
/// </summary>
internal sealed class RoleRepository :
    BaseRepository<Role>,
    IRoleRepository
{
    internal RoleRepository(FuDeverContext context) : base(context: context)
    {
    }
}
