using Domain.Entities;
using Domain.Repositories;
using Persistence.Database.SqlServer.Data;
using Persistence.Database.SqlServer.Repositories.Base;

namespace Persistence.Database.SqlServer.Repositories;

/// <summary>
///     Implementation of hobby repository.
/// </summary>
internal sealed class HobbyRepository :
    BaseRepository<Hobby>,
    IHobbyRepository
{
    internal HobbyRepository(FuDeverContext context) : base(context: context)
    {
    }
}