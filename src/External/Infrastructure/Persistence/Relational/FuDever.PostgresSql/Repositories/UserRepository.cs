using FuDever.Domain.Entities;
using FuDever.Domain.Repositories;
using FuDever.PostgresSql.Data;
using FuDever.PostgresSql.Repositories.Base;

namespace FuDever.PostgresSql.Repositories;

/// <summary>
///     Implementation of user repository.
/// </summary>
internal sealed class UserRepository :
    BaseRepository<User>,
    IUserRepository
{
    internal UserRepository(FuDeverContext context) : base(context: context)
    {
    }
}
