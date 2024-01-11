using System.Threading;
using System.Threading.Tasks;
using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Domain.Data;

/// <summary>
///     Fu dever context for interacting with "FuDeverDb" database.
/// </summary>
public interface IFuDeverContext
{
    /// <summary>
    ///     Get the set of specific entity.
    /// </summary>
    /// <typeparam name="TEntity">
    ///     Entity (table) of the database.
    /// </typeparam>
    /// <returns>
    ///     Set of that entity.
    /// </returns>
    DbSet<TEntity> DomainSet<TEntity>() where TEntity :
        class,
        IBaseEntity;

    /// <summary>
    ///     Save changes to the database.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     Task containing result of operation.
    /// </returns>
    Task<int> CustomSaveChangeAsync(CancellationToken cancellationToken);

    /// <summary>
    ///     Return the database facade to interact with database.
    /// </summary>
    DatabaseFacade DatabaseFacade { get; }
}
