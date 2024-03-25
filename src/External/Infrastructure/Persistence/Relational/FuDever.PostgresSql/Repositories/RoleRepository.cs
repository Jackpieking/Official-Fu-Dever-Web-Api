using FuDever.Domain.Entities;
using FuDever.Domain.Repositories;
using FuDever.PostgresSql.Data;
using FuDever.PostgresSql.Repositories.Base;

namespace FuDever.PostgresSql.Repositories;

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
