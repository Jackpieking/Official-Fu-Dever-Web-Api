using FuDever.Domain.Entities;
using FuDever.Domain.Repositories.Base;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Domain.Repositories;

/// <summary>
///     Represent methods that encapsulate queries
///     to interact with "Roles" table.
/// </summary>
/// <remarks>
///     All repository interfaces must implement
///     <seealso cref="IBaseRepository{TEntity}"/> interface.
/// </remarks>
public interface IRoleRepository : IBaseRepository<Role>
{
    /// <summary>
    ///     Update role asynchronously and directly to database.
    /// </summary>
    /// <param name="roleId">
    ///     Id of updated role.
    /// </param>
    /// <param name="roleRemovedAt">
    ///     When is role removed.
    /// </param>
    /// <param name="roleRemovedBy">
    ///     Role is removed by whom.
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
    Task<int> BulkUpdateByRoleIdVer1Async(
        Guid roleId,
        DateTime roleRemovedAt,
        Guid roleRemovedBy,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Update role asynchronously and directly to database.
    /// </summary>
    /// <param name="roleId">
    ///     Id of updated role.
    /// </param>
    /// <param name="roleName">
    ///     Name of updated role.
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
    Task<int> BulkUpdateByRoleIdVer2Async(
        Guid roleId,
        string roleName,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Remove role asynchronously and directly to database.
    /// </summary>
    /// <param name="roleId">
    ///     Id of removed role.
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
    Task<int> BulkRemoveByRoleIdAsync(
        Guid roleId,
        CancellationToken cancellationToken);
}