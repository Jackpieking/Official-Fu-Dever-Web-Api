using FuDever.Domain.Entities;
using FuDever.Domain.Repositories;
using FuDever.SqlServer.Data;
using FuDever.SqlServer.Repositories.Base;

namespace FuDever.SqlServer.Repositories;

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
