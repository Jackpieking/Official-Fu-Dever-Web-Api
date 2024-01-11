using Domain.Data;
using Domain.Entities;
using Domain.Repositories;
using Persistence.SqlServer2016.Repositories.Base;

namespace Persistence.SqlServer2016.Repositories;

/// <summary>
///     Implementation of role repository.
/// </summary>
internal sealed class RoleRepository :
    BaseRepository<Role>,
    IRoleRepository
{
    internal RoleRepository(IFuDeverContext context) : base(context: context)
    {
    }
}
