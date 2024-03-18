using FuDever.Domain.Entities.Base;
using System;

namespace FuDever.Domain.Entities;

/// <summary>
///     Represent the "BlogTags" table.
/// </summary>
public sealed class BlogTag : IBaseEntity
{
    private BlogTag()
    {
    }

    public Guid BlogId { get; set; }

    public Guid SkillId { get; set; }

    // Navigation properties.
    public Skill Skill { get; set; }

    public Blog Blog { get; set; }
}