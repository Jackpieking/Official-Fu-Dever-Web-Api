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
    public Skill(
        string skillName,
        DateTime skillRemovedAt)
    {
        Id = Guid.NewGuid();
        Name = skillName;
        RemovedAt = skillRemovedAt;
        RemovedBy = Guid.Empty;
    }

    public Skill(
        string skillName,
        DateTime skillRemovedAt,
        Guid skillRemovedBy)
    {
        Id = Guid.NewGuid();
        Name = skillName;
        RemovedAt = skillRemovedAt;
        RemovedBy = skillRemovedBy;
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
}