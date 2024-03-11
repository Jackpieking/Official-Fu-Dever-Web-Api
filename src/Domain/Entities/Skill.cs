using Domain.Entities.Base;
using System;
using System.Collections.Generic;

namespace Domain.Entities;

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

    /// <summary>
    ///     Skill id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Skill name.
    /// </summary>
    public string Name { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    // Navigation collections.
    public IEnumerable<BlogTag> BlogTags { get; set; }

    public IEnumerable<UserSkill> UserSkills { get; set; }

    /// <summary>
    ///     Return an instance.
    /// </summary>
    /// <param name="skillId">
    ///     Id of skill.
    /// </param>
    /// <param name="skillName">
    ///     Skill name.
    /// </param>
    /// <param name="skillRemovedAt">
    ///     Skill is removed by whom.
    /// </param>
    /// <param name="skillRemovedBy">
    ///     When is skill removed.
    /// </param>
    /// <returns>
    ///     A new skill object.
    /// </returns>
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
    ///     Return an instance.
    /// </summary>
    /// <param name="skillId">
    ///     Id of skill.
    /// </param>
    /// <param name="skillRemovedBy">
    ///     When is skill removed.
    /// </param>
    /// <param name="skillRemovedAt">
    ///     Skill is removed by whom.
    /// </param>
    /// <returns>
    ///     A new skill object.
    /// </returns>
    public static Skill InitVer2(
        Guid skillId,
        Guid skillRemovedBy,
        DateTime skillRemovedAt)
    {
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
            RemovedAt = skillRemovedAt,
            RemovedBy = skillRemovedBy
        };
    }

    /// <summary>
    ///     Return an instance.
    /// </summary>
    /// <param name="skillName">
    ///     Skill name.
    /// <returns>
    ///     A new skill object.
    /// </returns>
    public static Skill InitVer3(string skillName)
    {
        // Validate skill name.
        if (string.IsNullOrWhiteSpace(value: skillName) ||
            skillName.Length > Metadata.Name.MaxLength ||
            skillName.Length < Metadata.Name.MinLength)
        {
            return default;
        }

        return new()
        {
            Name = skillName
        };
    }

    /// <summary>
    ///     Return an instance.
    /// </summary>
    /// <param name="skillId">
    ///     Id of skill.
    /// </param>
    /// <param name="skillName">
    ///     Skill name.
    /// </param>
    /// <returns>
    ///     A new skill object.
    /// </returns>
    public static Skill InitVer4(
        Guid skillId,
        string skillName)
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

        return new()
        {
            Id = skillId,
            Name = skillName
        };
    }

    /// <summary>
    ///     Return an instance.
    /// </summary>
    /// <param name="skillId">
    ///     Id of skill.
    /// </param>
    /// <returns>
    ///     A new skill object.
    /// </returns>
    public static Skill InitVer5(Guid skillId)
    {
        // Validate skill Id.
        if (skillId == Guid.Empty)
        {
            return default;
        }

        return new()
        {
            Id = skillId
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

            /// <summary>
            ///     Min value length.
            /// </summary>
            public const int MinLength = 2;
        }
    }
}