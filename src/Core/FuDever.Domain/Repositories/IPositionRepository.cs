using FuDever.Domain.Entities;
using FuDever.Domain.Repositories.Base;

namespace FuDever.Domain.Repositories;

/// <summary>
///     Represent methods that encapsulate queries
///     to interact with "Positions" table.
/// </summary>
/// <remarks>
///     All repository interfaces must implement
///     <seealso cref="IBaseRepository{TEntity}"/> interface.
/// </remarks>
public interface IPositionRepository : IBaseRepository<Position>
{
}