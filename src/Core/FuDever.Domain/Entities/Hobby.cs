using FuDever.Domain.Entities.Base;
using System;
using System.Collections.Generic;

namespace FuDever.Domain.Entities;

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

    public Guid Id { get; set; }

    public string Name { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    // Navigation collections.
    public IEnumerable<UserHobby> UserHobbies { get; set; }

    public static Hobby InitForSeeding(
        Guid hobbyId,
        string hobbyName,
        DateTime hobbyRemovedAt,
        Guid hobbyRemovedBy)
    {
        return new()
        {
            Id = hobbyId,
            Name = hobbyName,
            RemovedAt = hobbyRemovedAt,
            RemovedBy = hobbyRemovedBy
        };
    }

    public static Hobby InitFromDatabaseVer1(
        Guid hobbyId,
        string hobbyName)
    {
        return new()
        {
            Id = hobbyId,
            Name = hobbyName
        };
    }

    public static Hobby InitFromDatabaseVer2(
        Guid hobbyId,
        string hobbyName,
        DateTime hobbyRemovedAt,
        Guid hobbyRemovedBy)
    {
        return new()
        {
            Id = hobbyId,
            Name = hobbyName,
            RemovedAt = hobbyRemovedAt,
            RemovedBy = hobbyRemovedBy
        };
    }

    public static Hobby InitFromDatabaseVer3(Guid hobbyId)
    {
        return new()
        {
            Id = hobbyId
        };
    }

    public static Hobby InitFromDatabaseVer4(string hobbyName)
    {
        return new()
        {
            Name = hobbyName
        };
    }

    public static Hobby InitVer1(
        Guid hobbyId,
        string hobbyName,
        DateTime hobbyRemovedAt,
        Guid hobbyRemovedBy)
    {
        // Validate hobby name.
        if (string.IsNullOrWhiteSpace(value: hobbyName) ||
            hobbyName.Length > Metadata.Name.MaxLength ||
            hobbyName.Length < Metadata.Name.MinLength)
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
    ///     Represent metadata of property.
    /// </summary>
    public static class Metadata
    {
        public static class Name
        {
            public const int MaxLength = 100;

            public const int MinLength = 2;
        }
    }
}