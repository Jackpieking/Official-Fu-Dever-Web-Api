using System.Linq;
using Domain.Entities.Base;
using Domain.Specifications.Base;
using Microsoft.EntityFrameworkCore;

namespace Domain.Specifications.Others;

/// <summary>
///     Represent query constructor by evaluating specification.
/// </summary>
public static class SpecificationEvaluator
{
    /// <summary>
    ///     Apply and transform specification into linq.
    /// </summary>
    /// <typeparam name="TEntity">
    ///     Represent the table of the database or
    ///     in the simple term, entity of the system.
    /// </typeparam>
    /// <param name="queryable">
    ///     Table to query.
    /// </param>
    /// <param name="specification">
    ///     Specification to form linq.
    /// </param>
    /// <returns>
    ///     The queryable with the appended formed linq.
    /// </returns>
    public static IQueryable<TEntity> Apply<TEntity>(
        this IQueryable<TEntity> queryable,
        IBaseSpecification<TEntity> specification)
            where TEntity :
                class,
                IBaseEntity
    {
        if (!Equals(objA: specification.WhereExpression, objB: null))
        {
            queryable = queryable.Where(predicate: specification.WhereExpression);
        }

        if (!Equals(objA: specification.OrderByAscendingExpression, objB: null))
        {
            queryable = queryable.OrderBy(keySelector: specification.OrderByAscendingExpression);
        }

        if (!Equals(objA: specification.OrderByDescendingExpression, objB: null))
        {
            queryable = queryable.OrderByDescending(keySelector: specification.OrderByDescendingExpression);
        }

        if (!Equals(objA: specification.ThenOrderByAscendingExpressions, objB: null))
        {
            var orderedQueryable = queryable as IOrderedQueryable<TEntity>;

            queryable = orderedQueryable.ThenBy(keySelector: specification.ThenOrderByAscendingExpressions);
        }

        if (!Equals(objA: specification.ThenOrderByDescendingExpressions, objB: null))
        {
            var orderedQueryable = queryable as IOrderedQueryable<TEntity>;

            queryable = orderedQueryable.ThenByDescending(keySelector: specification.OrderByDescendingExpression);
        }

        if (!Equals(objA: specification.SelectExpression, objB: null))
        {
            queryable = queryable.Select(selector: specification.SelectExpression);
        }

        if (specification.IsAsNoTracking)
        {
            queryable = queryable.AsNoTracking();
        }

        if (specification.IsAsSplitQuery)
        {
            queryable = queryable.AsSplitQuery();
        }

        queryable = specification.IncludeExpressions.Aggregate(
            seed: queryable,
            func: (
                current,
                includeExpression) =>
            {
                return current.Include(navigationPropertyPath: includeExpression);
            });

        if (specification.SkipNumberOfEntities > default(int))
        {
            queryable = queryable.Skip(count: specification.SkipNumberOfEntities);
        }

        if (specification.TakeNumberOfEntities > default(int))
        {
            queryable = queryable.Take(count: specification.TakeNumberOfEntities);
        }

        return queryable;
    }
}
