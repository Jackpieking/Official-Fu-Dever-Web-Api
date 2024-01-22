using Domain.Entities.Base;
using System;
using System.Collections.Generic;

namespace Domain.Entities;

/// <summary>
///     Represent the "Hobbies" table.
/// </summary>
public sealed class Hobby :
    IBaseEntity,
    ITemporarilyRemovedEntity
{
    private Hobby()
    {
    }

    /// <summary>
    ///     Hobby id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Hobby name.
    /// </summary>
    public string Name { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    // Navigation collections.
    public IEnumerable<UserHobby> UserHobbies { get; set; }

    /// <summary>
    ///     Return an instance.
    /// </summary>
    /// <param name="hobbyId">
    ///     Id of hobby.
    /// </param>
    /// <param name="hobbyName">
    ///     Hobby name.
    /// </param>
    /// <param name="hobbyRemovedAt">
    ///     Hobby is removed by whom.
    /// </param>
    /// <param name="hobbyRemovedBy">
    ///     When is hobby removed.
    /// </param>
    /// <returns>
    ///     A new hobby object.
    /// </returns>
    public static Hobby Init(
        Guid hobbyId,
        string hobbyName,
        DateTime hobbyRemovedAt,
        Guid hobbyRemovedBy)
    {
        // Validate hobby name.
        if (string.IsNullOrWhiteSpace(value: hobbyName) ||
            hobbyName.Length > Metadata.Name.MaxLength)
        {
            return default;
        }

        // Validate hobby Id.
        if (hobbyId == Guid.Empty)
        {
            return default;
        }

        // Validate hobby removed by.
        if (hobbyRemovedBy == Guid.Empty)
        {
            return default;
        }

        return new()
        {
            Id = hobbyId,
            Name = hobbyName,
            RemovedAt = hobbyRemovedAt,
            RemovedBy = hobbyRemovedBy
        };
    }

    /// <summary>
    ///     Return an instance.
    /// </summary>
    /// <param name="hobbyId">
    ///     Id of hobby.
    /// </param>
    /// <param name="hobbyName">
    ///     Hobby name.
    /// </param>
    /// <returns>
    ///     A new hobby object.
    /// </returns>
    public static Hobby Init(
        Guid hobbyId,
        string hobbyName)
    {
        // Validate hobby name.
        if (string.IsNullOrWhiteSpace(value: hobbyName) ||
            hobbyName.Length > Metadata.Name.MaxLength)
        {
            return default;
        }

        // Validate hobby Id.
        if (hobbyId == Guid.Empty)
        {
            return default;
        }

        return new()
        {
            Id = hobbyId,
            Name = hobbyName
        };
    }

    /// <summary>
    ///     Return an instance.
    /// </summary>
    /// <param name="hobbyId">
    ///     Id of hobby.
    /// </param>
    /// <returns>
    ///     A new hobby object.
    /// </returns>
    public static Hobby Init(Guid hobbyId)
    {
        // Validate hobby Id.
        if (hobbyId == Guid.Empty)
        {
            return default;
        }

        return new()
        {
            Id = hobbyId
        };
    }

    /// <summary>
    ///     Return an instance.
    /// </summary>
    /// <param name="hobbyName">
    ///     Hobby name.
    /// </param>
    /// <returns>
    ///     A new hobby object.
    /// </returns>
    public static Hobby Init(string hobbyName)
    {
        // Validate hobby name.
        if (string.IsNullOrWhiteSpace(value: hobbyName) ||
            hobbyName.Length > Metadata.Name.MaxLength)
        {
            return default;
        }

        return new()
        {
            Name = hobbyName
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