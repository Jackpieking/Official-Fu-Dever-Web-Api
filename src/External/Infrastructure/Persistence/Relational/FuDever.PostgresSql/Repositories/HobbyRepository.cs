using FuDever.Domain.Entities;
using FuDever.Domain.Repositories;
using FuDever.PostgresSql.Data;
using FuDever.PostgresSql.Repositories.Base;

namespace FuDever.PostgresSql.Repositories;

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