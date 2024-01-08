using System;

namespace Domain.Specifications.Entities.Department.Manager;

/// <summary>
///     Represent department specification manager.
/// </summary>
public interface IDepartmentSpecificationManager
{
    /// <summary>
    ///     Department as no tracking specification.
    /// </summary>
    IDepartmentAsNoTrackingSpecification DepartmentAsNoTrackingSpecification { get; }

    /// <summary>
    ///     Department not temporarily removed specification.
    /// </summary>
    IDepartmentNotTemporarilyRemovedSpecification DepartmentNotTemporarilyRemovedSpecification { get; }

    /// <summary>
    ///     Department temporarily removed specification.
    /// </summary>
    IDepartmentTemporarilyRemovedSpecification DepartmentTemporarilyRemovedSpecification { get; }

    /// <summary>
    ///     Select field from department table specification.
    /// </summary>
    ISelectFieldsFromDepartmentSpecification SelectFieldsFromDepartmentSpecification { get; }

    /// <summary>
    ///     Department by id specification
    /// </summary>
    /// <param name="departmentId">
    ///     Department id for finding department.
    /// </param>
    /// <returns>
    ///     Specification.
    /// </returns>
    IDepartmentByIdSpecification DepartmentByIdSpecification(Guid departmentId);

    /// <summary>
    ///     Department by name specification.
    /// </summary>
    /// <param name="departmentName">
    ///     Department name for finding department.
    /// </param>
    /// <param name="isCaseSensitive">
    ///     Does department name need searching in a sensitive way.
    /// </param>
    /// <returns>
    ///     Specification.
    /// </returns>
    IDepartmentByNameSpecification DepartmentByNameSpecification(
        string departmentName,
        bool isCaseSensitive);
}
