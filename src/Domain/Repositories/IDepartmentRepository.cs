using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories.Base;

namespace Domain.Repositories;

/// <summary>
///     Represent methods that encapsulate queries
///     to interact with "Departments" table.
/// </summary>
/// <remarks>
///     All repository interfaces must implement
///     <seealso cref="IBaseRepository{TEntity}"/> interface.
/// </remarks>
public interface IDepartmentRepository : IBaseRepository<Department>
{
    /// <summary>
    ///     Update department asynchronously and directly to database.
    /// </summary>
    /// <param name="departmentId">
    ///     Id of updated department.
    /// </param>
    /// <param name="departmentRemovedAt">
    ///     When is department removed.
    /// </param>
    /// <param name="departmentRemovedBy">
    ///     Department is removed by whom.
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
    Task<int> BulkUpdateByDepartmentIdVer1Async(
        Guid departmentId,
        DateTime departmentRemovedAt,
        Guid departmentRemovedBy,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Update department asynchronously and directly to database.
    /// </summary>
    /// <param name="departmentId">
    ///     Id of updated department.
    /// </param>
    /// <param name="departmentName">
    ///     Name of updated department.
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
    Task<int> BulkUpdateByDepartmentIdVer2Async(
        Guid departmentId,
        string departmentName,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Remove department asynchronously and directly to database.
    /// </summary>
    /// <param name="departmentId">
    ///     Id of removed department.
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
    Task<int> BulkRemoveByDepartmentIdAsync(
        Guid departmentId,
        CancellationToken cancellationToken);
}