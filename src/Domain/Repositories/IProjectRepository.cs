using Domain.Entities;
using Domain.Repositories.Base;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Repositories;

/// <summary>
///     Represent methods that encapsulate queries
///     to interact with "Projects" table.
/// </summary>
/// <remarks>
///     All repository interfaces must implement
///     <seealso cref="IBaseRepository{TEntity}"/> interface.
/// </remarks>
public interface IProjectRepository : IBaseRepository<Project>
{
    /// <summary>
    ///     Remove project asynchronously and directly to database.
    /// </summary>
    /// <param name="authorId">
    ///     Author who will have projects removed.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     Number of rows that are effected by this query.
    /// </returns>
    /// <remarks>
    ///     This method will alter directly to database.
    ///     This mean calling "SaveChanges" or "SaveChangesAsync"
    ///     from database context having no effect. So, make sure
    ///     to wrap this method in a database transaction.
    ///
    ///     All transaction methods are situated in
    ///     <seealso cref="UnitOfWorks"/> interface.
    /// </remarks>
    Task<int> BulkRemoveByAuthorIdAsync(
        Guid authorId,
        CancellationToken cancellationToken);
}
