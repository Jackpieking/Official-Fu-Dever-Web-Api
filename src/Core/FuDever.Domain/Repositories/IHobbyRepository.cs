using FuDever.Domain.Entities;
using FuDever.Domain.Repositories.Base;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Domain.Repositories;

/// <summary>
///     Represent methods that encapsulate queries
///     to interact with "Hobbies" table.
/// </summary>
/// <remarks>
///     All repository interfaces must implement
///     <seealso cref="IBaseRepository{TEntity}"/> interface.
/// </remarks>
public interface IHobbyRepository : IBaseRepository<Hobby>
{
    /// <summary>
    ///     Update hobby asynchronously and directly to database.
    /// </summary>
    /// <param name="hobbyId">
    ///     Id of updated hobby.
    /// </param>
    /// <param name="hobbyName">
    ///     Name of updated hobby.
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
    Task<int> BulkUpdateByHobbyIdVer1Async(
        Guid hobbyId,
        string hobbyName,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Updates records in the database based on a given hobby ID and additional parameters.
    /// </summary>
    /// <param name="hobbyId">
    ///     The ID of the hobby to update.
    /// </param>
    /// <param name="hobbyRemovedAt">
    ///     The date and time the hobby was removed.
    /// </param>
    /// <param name="hobbyRemovedBy">
    ///     The ID of the user who removed the hobby.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token to monitor for cancellation requests.
    /// </param>
    /// <returns>
    ///     A task representing the asynchronous operation. 
    ///     The task result represents the number of 
    ///     affected records.
    /// </returns>
    Task<int> BulkUpdateByHobbyIdVer2Async(
        Guid hobbyId,
        DateTime hobbyRemovedAt,
        Guid hobbyRemovedBy,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Removes multiple records from the database 
    ///     table based on the provided hobby ID.
    /// </summary>
    /// <param name="hobbyId">
    ///     The hobby ID to filter the records.
    /// </param>
    /// <param name="cancellationToken">
    ///     The token to monitor for cancellation requests.
    /// </param>
    /// <returns>
    ///     A task representing the asynchronous operation, 
    ///     which returns the number of records removed.
    /// </returns>
    Task<int> BulkRemoveByHobbyIdAsync(
        Guid hobbyId,
        CancellationToken cancellationToken);
}