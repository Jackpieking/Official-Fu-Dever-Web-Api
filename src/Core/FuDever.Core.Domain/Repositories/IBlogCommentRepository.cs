using FuDever.Domain.Entities;
using FuDever.Domain.Repositories.Base;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Domain.Repositories;

/// <summary>
///     Represent methods that encapsulate queries
///     to interact with "BlogComments" table.
/// </summary>
/// <remarks>
///     All repository interfaces must implement
///     <seealso cref="IBaseRepository{TEntity}"/> interface.
/// </remarks>
public interface IBlogCommentRepository : IBaseRepository<BlogComment>
{
    /// <summary>
    ///     Remove blog comment asynchronously and directly to database.
    /// </summary>
    /// <param name="blogCommentAuthorId">
    ///     Author who will have blog comments removed.
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
    ///     <para>
    ///         This method will alter directly to database.
    ///         This mean calling "SaveChanges" or "SaveChangesAsync"
    ///         from database context having no effect. So, make sure
    ///         to wrap this method in a database transaction.
    ///     </para>
    ///     <para>
    ///         All transaction methods are situated in
    ///         <seealso cref="UnitOfWorks.IUnitOfWork"/> interface.
    ///     </para>
    /// </remarks>
    public Task<int> BulkRemoveByAuthorIdAsync(
        Guid blogCommentAuthorId,
        CancellationToken cancellationToken);
}