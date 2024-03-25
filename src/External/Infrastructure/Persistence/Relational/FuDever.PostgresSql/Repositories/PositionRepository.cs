using FuDever.Domain.Entities;
using FuDever.Domain.Repositories;
using FuDever.PostgresSql.Data;
using FuDever.PostgresSql.Repositories.Base;

namespace FuDever.PostgresSql.Repositories;

/// <summary>
///     Implementation of position repository.
/// </summary>
internal sealed class PositionRepository :
    BaseRepository<Position>,
    IPositionRepository
{
    internal PositionRepository(FuDeverContext context) : base(context: context)
    {
    }
}
