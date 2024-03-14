using FuDever.Domain.Entities.Base;
using FuDever.Domain.Specifications.Base;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Linq.Expressions;

namespace FuDever.Domain.Specifications.Others;

/// <summary>
///     Represent specification extension method like
///     or, and, etc.
/// </summary>
public static class SpecificationExtension
{
    /// <summary>
    ///     Combine 2 specifications containing the where condition
    ///     into the or condition between 2 where conditions.
    /// </summary>
    /// <typeparam name="TEntity">
    ///     Represent the table of the database or
    ///     in the simple term, entity of the system.
    /// </typeparam>
    /// <param name="leftSpecification">
    ///     Contain the left expression.
    /// </param>
    /// <param name="rightSpecification">
    ///     Contain the right expression.
    /// </param>
    /// <returns>
    ///     A temporary expression containing the or result.
    /// </returns>
    public static TemporarySpecification<TEntity> Or<TEntity>(
        IBaseSpecification<TEntity> leftSpecification,
        IBaseSpecification<TEntity> rightSpecification)
            where TEntity :
                class,
                IBaseEntity
    {
        var leftExpression = leftSpecification.WhereExpression;
        var rightExpression = rightSpecification.WhereExpression;
        Expression rightBody;

        if (!ReferenceEquals(
                objA: rightExpression.Parameters[default],
                objB: leftExpression.Parameters[default]))
        {
            ReplacingExpressionVisitor visitor = new(
                originals: rightExpression.Parameters,
                replacements: leftExpression.Parameters);

            rightBody = visitor.Visit(expression: rightExpression.Body);
        }
        else
        {
            rightBody = rightExpression.Body;
        }

        var orExpression = Expression.OrElse(
            left: leftExpression.Body,
            right: rightBody);

        return new(whereExpression: Expression.Lambda<Func<TEntity, bool>>(
            body: orExpression,
            parameters: leftExpression.Parameters[default]));
    }

    /// <summary>
    ///     Combine 2 specifications containing the where condition
    ///     into the and condition between 2 where conditions.
    /// </summary>
    /// <typeparam name="TEntity">
    ///     Represent the table of the database or
    ///     in the simple term, entity of the system.
    /// </typeparam>
    /// <param name="leftSpecification">
    ///     Contain the left expression.
    /// </param>
    /// <param name="rightSpecification">
    ///     Contain the right expression.
    /// </param>
    /// <returns>
    ///     A temporary expression containing the and result.
    /// </returns>
    public static TemporarySpecification<TEntity> And<TEntity>(
        IBaseSpecification<TEntity> leftSpecification,
        IBaseSpecification<TEntity> rightSpecification)
            where TEntity :
                class,
                IBaseEntity
    {
        var leftExpression = leftSpecification.WhereExpression;
        var rightExpression = rightSpecification.WhereExpression;
        Expression rightBody;

        if (!ReferenceEquals(
                objA: rightExpression.Parameters[default],
                objB: leftExpression.Parameters[default]))
        {
            ReplacingExpressionVisitor visitor = new(
                originals: rightExpression.Parameters,
                replacements: leftExpression.Parameters);

            rightBody = visitor.Visit(expression: rightExpression.Body);
        }
        else
        {
            rightBody = rightExpression.Body;
        }

        var orExpression = Expression.AndAlso(
            left: leftExpression.Body,
            right: rightBody);

        return new(whereExpression: Expression.Lambda<Func<TEntity, bool>>(
            body: orExpression,
            parameters: leftExpression.Parameters[default]));
    }
}
