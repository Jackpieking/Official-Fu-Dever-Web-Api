using FuDever.Domain.Entities.Base;
using System;
using System.Collections.Generic;

namespace FuDever.Domain.Entities;

/// <summary>
///     Represent the "Skills" table.
/// </summary>
public sealed class Skill :
    IBaseEntity,
    ITemporarilyRemovedEntity
{
    private Skill()
    {
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    // Navigation collections.
    public IEnumerable<BlogTag> BlogTags { get; set; }

    public IEnumerable<UserSkill> UserSkills { get; set; }

    public static Skill InitForSeeding(
        Guid skillId,
        string skillName,
        DateTime skillRemovedAt,
        Guid skillRemovedBy)
    {
        return new()
        {
            Id = skillId,
            Name = skillName,
            RemovedAt = skillRemovedAt,
            RemovedBy = skillRemovedBy
        };
    }

    public static Skill InitFromDatabaseVer3(string skillName)
    {
        return new()
        {
            Name = skillName
        };
    }

    public static Skill InitFromDatabaseVer1(
        Guid skillId,
        string skillName)
    {
        return new()
        {
            Id = skillId,
            Name = skillName
        };
    }

    public static Skill InitFromDatabaseVer2(Guid skillId)
    {
        return new()
        {
            Id = skillId
        };
    }

    public static Skill InitFromDatabaseVer4(
        Guid skillId,
        string skillName,
        DateTime skillRemovedAt,
        Guid skillRemovedBy)
    {
        return new()
        {
            Id = skillId,
            Name = skillName,
            RemovedAt = skillRemovedAt,
            RemovedBy = skillRemovedBy
        };
    }

    public static Skill InitFromDatabaseVer5(
        Guid skillId,
        Guid skillRemovedBy,
        DateTime skillRemovedAt)
    {
        return new()
        {
            Id = skillId,
            RemovedAt = skillRemovedAt,
            RemovedBy = skillRemovedBy
        };
    }

    public static Skill InitVer1(
        Guid skillId,
        string skillName,
        DateTime skillRemovedAt,
        Guid skillRemovedBy)
    {
        // Validate skill name.
        if (string.IsNullOrWhiteSpace(value: skillName) ||
            skillName.Length > Metadata.Name.MaxLength ||
            skillName.Length < Metadata.Name.MinLength)
        {
            return default;
        }

        // Validate skill Id.
        if (skillId == Guid.Empty)
        {
            return default;
        }

        // Validate skill removed by.
        if (skillRemovedBy == Guid.Empty)
        {
            return default;
        }

        return new()
        {
            Id = skillId,
            Name = skillName,
            RemovedAt = skillRemovedAt,
            RemovedBy = skillRemovedBy
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

            public const int MinLength = 1;
        }
    }
}