using Domain.Entities;
using Domain.Repositories.Base;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Repositories;

/// <summary>
///     Represent methods that encapsulate queries
///     to interact with "Users" table.
/// </summary>
/// <remarks>
///     All repository interfaces must implement
///     <seealso cref="IBaseRepository{TEntity}"/> interface.
/// </remarks>
public interface IUserRepository : IBaseRepository<User>
{
    /// <summary>
    ///     Update user asynchronously and directly to database.
    /// </summary>
    /// <param name="userId">
    ///     Id of updated user.
    /// </param>
    /// <param name="userUpdatedAt">
    ///     When is user updated.
    /// </param>
    /// <param name="userUpdatedBy">
    ///     User is updated by whom.
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
    ///     <seealso cref="UnitOfWorks"/> interfaces
    /// </remarks>
    Task<int> BulkUpdateByUserIdVer1Async(
        Guid userId,
        DateTime userUpdatedAt,
        Guid userUpdatedBy,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Update user asynchronously and directly to database.
    /// </summary>
    /// <param name="userId">
    ///     Id of updated user.
    /// </param>
    /// <param name="userUpdatedAt">
    ///     When is user updated.
    /// </param>
    /// <param name="userUpdatedBy">
    ///     User is updated by whom.
    /// </param>
    /// <param name="positionId">
    ///     Position id of user.
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
    ///     <seealso cref="UnitOfWorks"/> interfaces
    /// </remarks>
    Task<int> BulkUpdateByUserIdVer2Async(
        Guid userId,
        DateTime userUpdatedAt,
        Guid userUpdatedBy,
        Guid positionId,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Update user asynchronously and directly to database.
    /// </summary>
    /// <param name="userId">
    ///     Id of updated user.
    /// </param>
    /// <param name="accessFailedCount">
    ///     How many times did user sign in fail.
    /// </param>
    /// <param name="lockoutEnd">
    ///     When does user temporarily lockout end.
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
    ///     <seealso cref="UnitOfWorks"/> interfaces
    /// </remarks>
    Task<int> BulkUpdateByUserIdVer3Async(
        Guid userId,
        int accessFailedCount,
        DateTime lockoutEnd,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Update user asynchronously and directly to database.
    /// </summary>
    /// <param name="userId">
    ///     Id of updated user.
    /// </param>
    /// <param name="accessFailedCount">
    ///     How many times did user sign in fail.
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
    ///     <seealso cref="UnitOfWorks"/> interfaces
    /// </remarks>
    Task<int> BulkUpdateByUserIdVer4Async(
        Guid userId,
        int accessFailedCount,
        CancellationToken cancellationToken);
}