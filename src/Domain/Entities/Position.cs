using Domain.Entities.Base;
using System;
using System.Collections.Generic;

namespace Domain.Entities;

/// <summary>
///     Represent the "Positions" table.
/// </summary>
public sealed class Position :
    IBaseEntity,
    ITemporarilyRemovedEntity
{
    private Position()
    {
    }

    /// <summary>
    ///     Position id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Position name.
    /// </summary>
    public string Name { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    // Navigation collections.
    public IEnumerable<User> Users { get; set; }

    /// <summary>
    ///     Return an instance.
    /// </summary>
    /// <param name="positionId">
    ///     Id of position.
    /// </param>
    /// <param name="positionName">
    ///     Position name.
    /// </param>
    /// <param name="positionRemovedAt">
    ///     Position is removed by whom.
    /// </param>
    /// <param name="positionRemovedBy">
    ///     When is position removed.
    /// </param>
    /// <returns>
    ///     A new position object.
    /// </returns>
    public static Position InitVer1(
        Guid positionId,
        string positionName,
        DateTime positionRemovedAt,
        Guid positionRemovedBy)
    {
        // Validate position name.
        if (string.IsNullOrWhiteSpace(value: positionName) ||
            positionName.Length > Metadata.Name.MaxLength)
        {
            return default;
        }

        // Validate position Id.
        if (positionId == Guid.Empty)
        {
            return default;
        }

        // Validate position removed by.
        if (positionRemovedBy == Guid.Empty)
        {
            return default;
        }

        return new()
        {
            Id = positionId,
            Name = positionName,
            RemovedAt = positionRemovedAt,
            RemovedBy = positionRemovedBy
        };
    }

    /// <summary>
    ///     Return an instance.
    /// </summary>
    /// <param name="positionName">
    ///     Position name.
    /// </param>
    /// <returns>
    ///     A new position object.
    /// </returns>
    public static Position InitVer2(string positionName)
    {
        // Validate position name.
        if (string.IsNullOrWhiteSpace(value: positionName) ||
            positionName.Length > Metadata.Name.MaxLength)
        {
            return default;
        }

        return new()
        {
            Name = positionName,
        };
    }

    /// <summary>
    ///     Return an instance.
    /// </summary>
    /// <param name="positionId">
    ///     Id of position.
    /// </param>
    /// <param name="positionName">
    ///     Position name.
    /// </param>
    /// <returns>
    ///     A new position object.
    /// </returns>
    public static Position InitVer3(
        Guid positionId,
        string positionName)
    {
        // Validate position name.
        if (string.IsNullOrWhiteSpace(value: positionName) ||
            positionName.Length > Metadata.Name.MaxLength)
        {
            return default;
        }

        // Validate position Id.
        if (positionId == Guid.Empty)
        {
            return default;
        }

        return new()
        {
            Id = positionId,
            Name = positionName
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