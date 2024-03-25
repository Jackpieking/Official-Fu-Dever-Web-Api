using FuDever.Domain.Entities;
using FuDever.Domain.Repositories;
using FuDever.PostgresSql.Data;
using FuDever.PostgresSql.Repositories.Base;

namespace FuDever.PostgresSql.Repositories;

/// <summary>
///     Implementation of user role repository.
/// </summary>
internal sealed class UserRoleRepository :
    BaseRepository<UserRole>,
    IUserRoleRepository
{
    public UserRoleRepository(FuDeverContext context) : base(context: context)
    {
    }
}
