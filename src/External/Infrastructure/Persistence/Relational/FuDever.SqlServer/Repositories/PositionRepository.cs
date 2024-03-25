using FuDever.Domain.Entities;
using FuDever.Domain.Repositories;
using FuDever.SqlServer.Data;
using FuDever.SqlServer.Repositories.Base;

namespace FuDever.SqlServer.Repositories;

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
