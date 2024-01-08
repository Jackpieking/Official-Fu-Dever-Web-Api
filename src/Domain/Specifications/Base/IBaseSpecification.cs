using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Domain.Specifications.Base;

/// <summary>
///     Represent the base specification that
///     every specification that is created must inherit
///     from this interface.
/// </summary>
/// <typeparam name="TEntity">
///     Represent the table of the database or in the simple term,
///     entity of the app.
/// </typeparam>
public interface IBaseSpecification<TEntity> where TEntity :
    class,
    IBaseEntity
{
    /// <summary>
    ///     Expression that is used for "Where" method.
    /// </summary>
    Expression<Func<TEntity, bool>> WhereExpression { get; set; }

    /// <summary>
    ///     Expression that is used for "OrderBy" method.
    /// </summary>
    Expression<Func<TEntity, object>> OrderByAscendingExpression { get; set; }

    /// <summary>
    ///     Expression that is used for "OrderByDescending" method.
    /// </summary>
    Expression<Func<TEntity, object>> OrderByDescendingExpression { get; set; }

    /// <summary>
    ///     Expression that is used for "Select" method.
    /// </summary>
    Expression<Func<TEntity, TEntity>> SelectExpression { get; set; }

    /// <summary>
    ///     Boolean value that is used for "AsSplitQuery" method.
    /// </summary>
    bool IsAsSplitQuery { get; set; }

    /// <summary>
    ///     Boolean value that is used for "AsNoTracking" method.
    /// </summary>
    bool IsAsNoTracking { get; set; }

    /// <summary>
    ///     Expression that is used for "ThenBy" method.
    /// </summary>
    Expression<Func<TEntity, object>> ThenOrderByAscendingExpressions { get; set; }

    /// <summary>
    ///     Expression that is used for "ThenByDescending" method.
    /// </summary>
    Expression<Func<TEntity, object>> ThenOrderByDescendingExpressions { get; set; }

    /// <summary>
    ///     Expressions that is used for constructing
    ///     multiple "Include" methods follow the number of expressions.
    /// </summary>
    List<Expression<Func<TEntity, object>>> IncludeExpressions { get; }

    /// <summary>
    ///     Int value that is used for "Skip" method.
    /// </summary>
    int SkipNumberOfEntities { get; set; }

    /// <summary>
    ///     Int value that is used for "Take" method.
    /// </summary>
    int TakeNumberOfEntities { get; set; }
}
