using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces.Data;

/// <summary>
///     Db context for interacting with database.
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
}
