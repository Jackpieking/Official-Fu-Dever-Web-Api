using FuDever.Domain.Entities.Base;

namespace FuDever.Domain.EntityBuilders.Others;

public interface IBaseEntityHandler<out TEntity>
    where TEntity : IBaseEntity
{
    /// <summary>
    ///     Complete building entity.
    /// </summary>
    /// <returns>
    ///     Entity with supplied fields.
    /// </returns>
    TEntity Complete();
}
