using Domain.Entities.Base;
using System;
using System.Collections.Generic;

namespace Domain.Entities;

/// <summary>
///     Represent the "Majors" table.
/// </summary>
public sealed class Major :
    IBaseEntity,
    ITemporarilyRemovedEntity
{
    private Major()
    {
    }

    /// <summary>
    ///     Major id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Major name.
    /// </summary>
    public string Name { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    // Navigation collections.
    public IEnumerable<User> Users { get; set; }

    /// <summary>
    ///     Return an instance.
    /// </summary>
    /// <param name="majorId">
    ///     Id of major.
    /// </param>
    /// <param name="majorName">
    ///     Major name.
    /// </param>
    /// <param name="majorRemovedAt">
    ///     Major is removed by whom.
    /// </param>
    /// <param name="majorRemovedBy">
    ///     When is major removed.
    /// </param>
    /// <returns>
    ///     A new major object.
    /// </returns>
    public static Major InitVer1(
        Guid majorId,
        string majorName,
        DateTime majorRemovedAt,
        Guid majorRemovedBy)
    {
        // Validate major name.
        if (string.IsNullOrWhiteSpace(value: majorName) ||
            majorName.Length > Metadata.Name.MaxLength)
        {
            return default;
        }

        // Validate major Id.
        if (majorId == Guid.Empty)
        {
            return default;
        }

        // Validate major removed by.
        if (majorRemovedBy == Guid.Empty)
        {
            return default;
        }

        return new()
        {
            Id = majorId,
            Name = majorName,
            RemovedAt = majorRemovedAt,
            RemovedBy = majorRemovedBy
        };
    }

    /// <summary>
    ///     Return an instance.
    /// </summary>
    /// <param name="majorId">
    ///     Id of major.
    /// </param>
    /// <param name="majorName">
    ///     Major name.
    /// </param>
    /// <returns>
    ///     A new major object.
    /// </returns>
    public static Major InitVer2(
        Guid majorId,
        string majorName)
    {
        // Validate major name.
        if (string.IsNullOrWhiteSpace(value: majorName) ||
            majorName.Length > Metadata.Name.MaxLength)
        {
            return default;
        }

        // Validate major Id.
        if (majorId == Guid.Empty)
        {
            return default;
        }

        return new()
        {
            Id = majorId,
            Name = majorName
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