using Domain.Entities.Base;
using Domain.Specifications.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Repositories.Base;

/// <summary>
///     Represent the base repository that every
///     repository that is created must inherit from
///     this interface.
/// </summary>
/// <typeparam name="TEntity">
///     Represent the table of the database or
///     in the simple term, entity of the system.
/// </typeparam>
/// <remarks>
///     All repository interfaces must inherit from
///     this base interface.
/// </remarks>
public interface IBaseRepository<TEntity> where TEntity :
    class,
    IBaseEntity
{
    /// <summary>
    ///     Asynchronously change the state of entity to
    ///     <seealso cref="EntityState.Added"/>.
    /// </summary>
    /// <param name="newEntity">
    ///     The entity for adding to the database.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     A task containing result of operation.
    /// </returns>
    Task AddAsync(
        TEntity newEntity,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Asynchronously change the state of list of
    ///     entities to <seealso cref="EntityState.Added"/>.
    /// </summary>
    /// <param name="newEntities">
    ///     List of entities for adding to the database.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     A task containing result of operation.
    /// </returns>
    Task AddRangeAsync(
        IEnumerable<TEntity> newEntities,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Asynchronously check the existence of the entity.
    /// </summary>
    /// <param name="specifications">
    ///     List of specifications that are used for
    ///     constructing a complete query.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     A task containing a boolean result of operation.
    /// </returns>
    Task<bool> IsFoundBySpecificationsAsync(
        IEnumerable<IBaseSpecification<TEntity>> specifications,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Synchronously get a list of entity which satisfies
    ///     the specifications.
    /// </summary>
    /// <param name="specifications">
    ///     List of specifications that are used for
    ///     constructing a complete query.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     A task containing list of found entities.
    /// </returns>
    Task<IEnumerable<TEntity>> GetAllBySpecificationsAsync(
        IEnumerable<IBaseSpecification<TEntity>> specifications,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Asynchronously find the entity which satisfies
    ///     the specifications.
    /// </summary>
    /// <param name="specifications">
    ///     List of specifications that are used for
    ///     constructing a complete query.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     A task containing the found entity.
    /// </returns>
    Task<TEntity> FindBySpecificationsAsync(
        IEnumerable<IBaseSpecification<TEntity>> specifications,
        CancellationToken cancellationToken);
}
