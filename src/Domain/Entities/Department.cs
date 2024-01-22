using Domain.Entities.Base;
using System;
using System.Collections.Generic;

namespace Domain.Entities;

/// <summary>
///     Represent the "Departments" table.
/// </summary>
public sealed class Department :
    IBaseEntity,
    ITemporarilyRemovedEntity
{
    private Department()
    {
    }

    /// <summary>
    ///     Department id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Department name.
    /// </summary>
    public string Name { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    // Navigation collections.
    public IEnumerable<User> Users { get; set; }

    /// <summary>
    ///     Return an instance.
    /// </summary>
    /// <param name="departmentId">
    ///     Id of department.
    /// </param>
    /// <param name="departmentName">
    ///     Department name.
    /// </param>
    /// <param name="departmentRemovedAt">
    ///     Department is removed by whom.
    /// </param>
    /// <param name="departmentRemovedBy">
    ///     When is department removed.
    /// </param>
    /// <returns>
    ///     A new department object.
    /// </returns>
    public static Department Init(
        Guid departmentId,
        string departmentName,
        DateTime departmentRemovedAt,
        Guid departmentRemovedBy)
    {
        // Validate department name.
        if (string.IsNullOrWhiteSpace(value: departmentName) ||
            departmentName.Length > Metadata.Name.MaxLength)
        {
            return default;
        }

        // Validate department Id.
        if (departmentId == Guid.Empty)
        {
            return default;
        }

        // Validate department removed by.
        if (departmentRemovedBy == Guid.Empty)
        {
            return default;
        }

        return new()
        {
            Id = departmentId,
            Name = departmentName,
            RemovedAt = departmentRemovedAt,
            RemovedBy = departmentRemovedBy
        };
    }

    /// <summary>
    ///     Return an instance.
    /// </summary>
    /// <param name="departmentId">
    ///     Id of department.
    /// </param>
    /// <param name="departmentName">
    ///     Department name.
    /// </param>
    /// <returns>
    ///     A new department object.
    /// </returns>
    public static Department Init(
        Guid departmentId,
        string departmentName)
    {
        // Validate department name.
        if (string.IsNullOrWhiteSpace(value: departmentName) ||
            departmentName.Length > Metadata.Name.MaxLength)
        {
            return default;
        }

        // Validate department Id.
        if (departmentId == Guid.Empty)
        {
            return default;
        }

        return new()
        {
            Id = departmentId,
            Name = departmentName
        };
    }

    /// <summary>
    ///     Return an instance.
    /// </summary>
    /// <param name="departmentName">
    ///     Department name.
    /// </param>
    /// <returns>
    ///     A new department object.
    /// </returns>
    public static Department Init(string departmentName)
    {
        // Validate department name.
        if (string.IsNullOrWhiteSpace(value: departmentName) ||
            departmentName.Length > Metadata.Name.MaxLength)
        {
            return default;
        }

        return new()
        {
            Name = departmentName
        };
    }

    /// <summary>
    ///     Represent metadata of property.
    /// </summary>
    public static class Metadata
    {
        /// <summary>
        ///     Name property.
        /// </summary>
        public static class Name
        {
            /// <summary>
            ///     Max value length.
            /// </summary>
            public const int MaxLength = 100;
        }
    }
}