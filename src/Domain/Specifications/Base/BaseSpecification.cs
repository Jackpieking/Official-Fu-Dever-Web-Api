using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Domain.Specifications.Base;

/// <summary>
///     Represent implementation of base specification.
/// </summary>
/// <typeparam name="TEntity">
///     Represent the table of the database or
///     in the simple term, entity of the system.
/// </typeparam>
public abstract class BaseSpecification<TEntity> : IBaseSpecification<TEntity>
    where TEntity :
        class,
        IBaseEntity
{
    // Backing fields.
    private List<Expression<Func<TEntity, object>>> _includeExpressions;
    private int _skipNumberOfEntities;
    private int _takeNumberOfEntities;

    public Expression<Func<TEntity, bool>> WhereExpression { get; set; }

    public Expression<Func<TEntity, object>> OrderByAscendingExpression { get; set; }

    public Expression<Func<TEntity, object>> OrderByDescendingExpression { get; set; }

    public Expression<Func<TEntity, object>> ThenOrderByAscendingExpressions { get; set; }

    public Expression<Func<TEntity, object>> ThenOrderByDescendingExpressions { get; set; }

    public Expression<Func<TEntity, TEntity>> SelectExpression { get; set; }

    public bool IsAsSplitQuery { get; set; }

    public bool IsAsNoTracking { get; set; }

    public List<Expression<Func<TEntity, object>>> IncludeExpressions
    {
        get
        {
            _includeExpressions ??= [];

            return _includeExpressions;
        }
    }

    public int SkipNumberOfEntities
    {
        get => _skipNumberOfEntities;
        set
        {
            if (value <= default(int))
            {
                _skipNumberOfEntities = default;

                return;
            }

            _skipNumberOfEntities = value;
        }
    }

    public int TakeNumberOfEntities
    {
        get => _takeNumberOfEntities;
        set
        {
            if (value <= default(int))
            {
                _takeNumberOfEntities = default;

                return;
            }

            _takeNumberOfEntities = value;
        }
    }
}
