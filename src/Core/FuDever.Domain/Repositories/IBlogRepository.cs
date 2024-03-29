using FuDever.Domain.Entities;
using FuDever.Domain.Repositories.Base;

namespace FuDever.Domain.Repositories;

/// <summary>
///     Represent methods that encapsulate queries
///     to interact with "Blogs" table.
/// </summary>
/// <remarks>
///     All repository interfaces must implement
///     <seealso cref="IBaseRepository{TEntity}"/> interface.
/// </remarks>
public interface IBlogRepository : IBaseRepository<Blog>
{
}