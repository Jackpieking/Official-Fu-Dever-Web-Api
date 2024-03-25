using FuDever.Domain.Entities.Base;
using System;
using System.Collections.Generic;

namespace FuDever.Domain.Entities;

/// <summary>
///     Represent the "Skills" table.
/// </summary>
public class Skill :
    IBaseEntity,
    ITemporarilyRemovedEntity
{
    internal Skill()
    {
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    // Navigation collections.
    public IEnumerable<BlogTag> BlogTags { get; set; }

    public IEnumerable<UserSkill> UserSkills { get; set; }

    public static class Metadata
    {
        public static class Name
        {
            public const int MaxLength = 100;

            public const int MinLength = 1;
        }
    }
}