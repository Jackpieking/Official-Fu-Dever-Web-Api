using FuDever.Domain.EntityBuilders.Skill.Others;
using System;

namespace FuDever.Domain.EntityBuilders.Skill;

/// <summary>
///     Skill for database seeding builder.
/// </summary>
public sealed class SkillForDatabaseSeedingBuilder :
    Entities.Skill,
    IBaseSkillBuilder,
    ISkillBuilder<SkillForDatabaseSeedingBuilder>
{
    public Entities.Skill Complete()
    {
        return new()
        {
            Id = Id,
            Name = Name,
            RemovedAt = RemovedAt,
            RemovedBy = RemovedBy
        };
    }

    public SkillForDatabaseSeedingBuilder WithId(Guid skillId)
    {
        // Validate skill Id.
        if (skillId == Guid.Empty)
        {
            return default;
        }

        Id = skillId;

        return this;
    }

    public SkillForDatabaseSeedingBuilder WithName(string skillName)
    {
        Name = skillName;

        return this;
    }

    public SkillForDatabaseSeedingBuilder WithRemovedAt(DateTime skillRemovedAt)
    {
        // Validate skill removed at.
        if (skillRemovedAt == DateTime.MaxValue)
        {
            return default;
        }

        RemovedAt = skillRemovedAt;

        return this;
    }

    public SkillForDatabaseSeedingBuilder WithRemovedBy(Guid skillRemovedBy)
    {
        // Validate skill removed by.
        if (skillRemovedBy == Guid.Empty)
        {
            return default;
        }

        RemovedBy = skillRemovedBy;

        return this;
    }
}
