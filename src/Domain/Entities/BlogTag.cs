using System;
using Domain.Entities.Base;

namespace Domain.Entities;

/// <summary>
///     Represent the "BlogTags" table.
/// </summary>
public sealed class BlogTag : IBaseEntity
{
    /// <summary>
    ///     Blog id.
    /// </summary>
    public Guid BlogId { get; set; }

    /// <summary>
    ///     Skill id.
    /// </summary>
    public Guid SkillId { get; set; }

    // Navigation properties.
    public Skill Skill { get; set; }

    public Blog Blog { get; set; }
}