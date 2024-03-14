using FuDever.Domain.Entities;
using FuDever.Domain.Repositories.Base;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Domain.Repositories;

/// <summary>
///     Represent methods that encapsulate queries
///     to interact with "Platforms" table.
/// </summary>
/// <remarks>
///     All repository interfaces must implement
///     <seealso cref="IBaseRepository{TEntity}"/> interface.
/// </remarks>
public interface IPlatformRepository : IBaseRepository<Platform>
{
    /// <summary>
    ///     Update platform asynchronously and directly to database.
    /// </summary>
    /// <param name="platformId">
    ///     Id of updated platform.
    /// </param>
    /// <param name="platformRemovedAt">
    ///     When is platform removed.
    /// </param>
    /// <param name="platformRemovedBy">
    ///     Platform is removed by whom.
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
    ///     <seealso cref="UnitOfWorks.IUnitOfWork"/> interface.
    /// </remarks>
    Task<int> BulkUpdateByPlatformIdVer1Async(
        Guid platformId,
        DateTime platformRemovedAt,
        Guid platformRemovedBy,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Update platform asynchronously and directly to database.
    /// </summary>
    /// <param name="platformId">
    ///     Id of updated platform.
    /// </param>
    /// <param name="platformName">
    ///     Name of updated platform.
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
    ///     <seealso cref="UnitOfWorks.IUnitOfWork"/> interface.
    /// </remarks>
    Task<int> BulkUpdateByPlatformIdVer2Async(
        Guid platformId,
        string platformName,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Remove platform asynchronously and directly to database.
    /// </summary>
    /// <param name="platformId">
    ///     Id of removed platform.
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
    ///     <seealso cref="UnitOfWorks.IUnitOfWork"/> interface.
    /// </remarks>
    Task<int> BulkRemoveByPlatformIdAsync(
        Guid platformId,
        CancellationToken cancellationToken);
}