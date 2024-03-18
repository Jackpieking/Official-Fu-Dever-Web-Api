using FuDever.Domain.Entities.Base;
using System;

namespace FuDever.Domain.Entities;

/// <summary>
///     Represent the "UserSkills" table.
/// </summary>
public sealed class UserSkill : IBaseEntity
{
    private UserSkill()
    {
    }

    public Guid UserId { get; set; }

    public Guid SkillId { get; set; }

    // Navigation properties.
    public User User { get; set; }

    public Skill Skill { get; set; }

    public static UserSkill InitFromDatabaseVer1(Skill skill)
    {
        return new()
        {
            Skill = skill
        };
    }

    public static UserSkill InitFromDatabaseVer2(
        Guid skillId,
        Skill skill)
    {
        return new()
        {
            SkillId = skillId,
            Skill = skill
        };
    }

    public static UserSkill InitFromDatabaseVer3(Guid userId)
    {
        return new()
        {
            UserId = userId
        };
    }
}