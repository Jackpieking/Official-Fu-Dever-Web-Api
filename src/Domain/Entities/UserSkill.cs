using Domain.Entities.Base;
using System;

namespace Domain.Entities;

/// <summary>
///     Represent the "UserSkills" table.
/// </summary>
public sealed class UserSkill : IBaseEntity
{
    private UserSkill()
    {
    }

    /// <summary>
    ///     User id.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    ///     Skill id.
    /// </summary>
    public Guid SkillId { get; set; }

    // Navigation properties.
    public User User { get; set; }

    public Skill Skill { get; set; }

    /// <summary>
    ///     Return an instance.
    /// </summary>
    /// <param name="skill">
    ///     Skill of user skill.
    /// </param>
    /// <returns>
    ///     A new user skill object.
    /// </returns>
    public static UserSkill InitVer1(Skill skill)
    {
        return new()
        {
            Skill = skill
        };
    }

    /// <summary>
    ///     Return an instance.
    /// </summary>
    /// <param name="skillId">
    ///     Skill id of user skill.
    /// </param>
    /// <param name="skill">
    ///     Skill of user skill.
    /// </param>
    /// <returns>
    ///     A new user skill object.
    /// </returns>
    public static UserSkill InitVer2(
        Guid skillId,
        Skill skill)
    {
        return new()
        {
            SkillId = skillId,
            Skill = skill
        };
    }

    /// <summary>
    ///     Return an instance.
    /// </summary>
    /// <param name="userId">
    ///     User id of user skill.
    /// </param>
    /// <returns>
    ///     A new user skill object.
    /// </returns>
    public static UserSkill InitVer3(Guid userId)
    {
        return new()
        {
            UserId = userId
        };
    }
}