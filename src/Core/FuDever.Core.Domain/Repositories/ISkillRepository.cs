using FuDever.Domain.Entities;
using FuDever.Domain.Repositories.Base;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Domain.Repositories;

/// <summary>
///     Represent methods that encapsulate queries
///     to interact with "Skills" table.
/// </summary>
/// <remarks>
///     All repository interfaces must implement
///     <seealso cref="IBaseRepository{TEntity}"/> interface.
/// </remarks>
public interface ISkillRepository : IBaseRepository<Skill>
{
    /// <summary>
    ///     Update skill asynchronously and directly to database.
    /// </summary>
    /// <param name="skillId">
    ///     Id of updated skill.
    /// </param>
    /// <param name="skillRemovedAt">
    ///     When is skill removed.
    /// </param>
    /// <param name="skillRemovedBy">
    ///     Skill is removed by whom.
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
    Task<int> BulkUpdateBySkillIdVer1Async(
        Guid skillId,
        DateTime skillRemovedAt,
        Guid skillRemovedBy,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Update skill asynchronously and directly to database.
    /// </summary>
    /// <param name="skillId">
    ///     Id of updated skill.
    /// </param>
    /// <param name="skillName">
    ///     Name of updated skill.
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
    Task<int> BulkUpdateBySkillIdVer2Async(
        Guid skillId,
        string skillName,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Remove skill asynchronously and directly to database.
    /// </summary>
    /// <param name="skillId">
    ///     Id of removed skill.
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
    Task<int> BulkRemoveBySkillIdAsync(
        Guid skillId,
        CancellationToken cancellationToken);
}