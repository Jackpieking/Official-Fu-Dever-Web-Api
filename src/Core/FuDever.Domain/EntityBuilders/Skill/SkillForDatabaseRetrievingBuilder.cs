using FuDever.Domain.EntityBuilders.Skill.Others;
using System;

namespace FuDever.Domain.EntityBuilders.Skill;

/// <summary>
///     Skill for database retrieving builder.
/// </summary>
public sealed class SkillForDatabaseRetrievingBuilder :
    Entities.Skill,
    IBaseSkillBuilder,
    ISkillBuilder<SkillForDatabaseRetrievingBuilder>
{
    public void Clear()
    {
        Id = Guid.Empty;
        Name = default;
        RemovedAt = default;
        RemovedBy = Guid.Empty;
    }

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

    public SkillForDatabaseRetrievingBuilder WithId(Guid skillId)
    {
        Id = skillId;

        return this;
    }

    public SkillForDatabaseRetrievingBuilder WithName(string skillName)
    {
        Name = skillName;

        return this;
    }

    public SkillForDatabaseRetrievingBuilder WithRemovedAt(DateTime skillRemovedAt)
    {
        RemovedAt = skillRemovedAt;

        return this;
    }

    public SkillForDatabaseRetrievingBuilder WithRemovedBy(Guid skillRemovedBy)
    {
        RemovedBy = skillRemovedBy;

        return this;
    }
}
