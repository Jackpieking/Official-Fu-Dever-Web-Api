using System;
using System.Collections.Generic;
using Domain.Entities.Base;

namespace Domain.Entities;

/// <summary>
///     Represent the "Skills" table.
/// </summary>
public sealed class Skill :
    IBaseEntity,
    ITemporarilyRemovedEntity
{
    private const int MaxSkillNameLength = 100;

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
    public static Skill Init(
        Guid skillId,
        string skillName,
        DateTime skillRemovedAt,
        Guid skillRemovedBy)
    {
        // Validate skill name.
        if (string.IsNullOrWhiteSpace(value: skillName) ||
            skillName.Length > MaxSkillNameLength)
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
    /// <param name="skillName">
    ///     Skill name.
    /// <returns>
    ///     A new skill object.
    /// </returns>
    public static Skill Init(string skillName)
    {
        // Validate skill name.
        if (string.IsNullOrWhiteSpace(value: skillName) ||
            skillName.Length > MaxSkillNameLength)
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
    public static Skill Init(
        Guid skillId,
        string skillName)
    {
        // Validate skill name.
        if (string.IsNullOrWhiteSpace(value: skillName) ||
            skillName.Length > MaxSkillNameLength)
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
    public static Skill Init(Guid skillId)
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
}