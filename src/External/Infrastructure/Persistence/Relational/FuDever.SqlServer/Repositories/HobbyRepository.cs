using FuDever.Domain.Entities;
using FuDever.Domain.Repositories;
using FuDever.SqlServer.Data;
using FuDever.SqlServer.Repositories.Base;

namespace FuDever.SqlServer.Repositories;

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