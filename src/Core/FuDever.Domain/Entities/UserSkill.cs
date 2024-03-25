using FuDever.Domain.Entities.Base;
using System;

namespace FuDever.Domain.Entities;

/// <summary>
///     Represent the "UserSkills" table.
/// </summary>
public class UserSkill : IBaseEntity
{
    internal UserSkill()
    {
    }

    public Guid UserId { get; set; }

    public Guid SkillId { get; set; }

    // Navigation properties.
    public User User { get; set; }

    public Skill Skill { get; set; }
}