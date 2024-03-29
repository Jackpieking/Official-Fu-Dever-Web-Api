using FuDever.Domain.Entities.Base;
using FuDever.Domain.Specifications.Base;
using System;
using System.Linq.Expressions;

namespace FuDever.Domain.Specifications.Others;

/// <summary>
///    Represent temporary specification.
/// </summary>
/// <typeparam name="TEntity">
///     Represent the table of the database or
///     in the simple term, entity of the system.
/// </typeparam>
public sealed class TemporarySpecification<TEntity> : BaseSpecification<TEntity>
    where TEntity :
        class,
        IBaseEntity
{
    /// <summary>
    ///     Construct the temporary expression.
    /// </summary>
    /// <param name="whereExpression">
    ///     Where expression to be contained in.
    /// </param>
    public TemporarySpecification(Expression<Func<TEntity, bool>> whereExpression)
    {
        WhereExpression = whereExpression;
    }
}
