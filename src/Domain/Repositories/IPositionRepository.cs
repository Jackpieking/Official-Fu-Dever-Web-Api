using Domain.Entities;
using Domain.Repositories.Base;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Repositories;

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
    /// <summary>
    ///     Update position asynchronously and directly to database.
    /// </summary>
    /// <param name="positionId">
    ///     Id of updated position.
    /// </param>
    /// <param name="positionRemovedAt">
    ///     When is position removed.
    /// </param>
    /// <param name="positionRemovedBy">
    ///     Position is removed by whom.
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
    Task<int> BulkUpdateByPositionIdVer1Async(
        Guid positionId,
        DateTime positionRemovedAt,
        Guid positionRemovedBy,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Update position asynchronously and directly to database.
    /// </summary>
    /// <param name="positionId">
    ///     Id of updated position.
    /// </param>
    /// <param name="positionName">
    ///     Name of updated position.
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
    Task<int> BulkUpdateByPositionIdVer2Async(
        Guid positionId,
        string positionName,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Remove position asynchronously and directly to database.
    /// </summary>
    /// <param name="positionId">
    ///     Id of removed position.
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
    Task<int> BulkRemoveByPositionIdAsync(
        Guid positionId,
        CancellationToken cancellationToken);
}