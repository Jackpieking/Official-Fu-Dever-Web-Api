using System;
using Domain.Entities.Base;

namespace Domain.Entities;

/// <summary>
///     Represent the "UserSkills" table.
/// </summary>
public sealed class UserSkill :
    IBaseEntity
{
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
}