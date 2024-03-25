using FuDever.Domain.EntityBuilders.Others;
using System;

namespace FuDever.Domain.EntityBuilders.Department.Others;

/// <summary>
///     Interface for department builder.
/// </summary>
public interface IDepartmentBuilder<TBuilder> :
    IBaseEntityHandler<Entities.Department>
        where TBuilder : IBaseDepartmentBuilder
{
    /// <summary>
    ///     Set the id of department.
    /// </summary>
    /// <param name="departmentId">
    ///     Id of department.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithId(Guid departmentId);

    /// <summary>
    ///     Set the name of department.
    /// </summary>
    /// <param name="departmentName">
    ///     Name of department.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithName(string departmentName);

    /// <summary>
    ///     Set the remove time of department.
    /// </summary>
    /// <param name="departmentRemovedAt">
    ///     Remove time of department.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithRemovedAt(DateTime departmentRemovedAt);

    /// <summary>
    ///     Set the remover of department.
    /// </summary>
    /// <param name="departmentRemovedBy">
    ///     Remover of department.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithRemovedBy(Guid departmentRemovedBy);
}
