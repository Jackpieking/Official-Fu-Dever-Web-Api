using FuDever.Domain.Entities;
using FuDever.Domain.Repositories;
using FuDever.SqlServer.Data;
using FuDever.SqlServer.Repositories.Base;

namespace FuDever.SqlServer.Repositories;

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
