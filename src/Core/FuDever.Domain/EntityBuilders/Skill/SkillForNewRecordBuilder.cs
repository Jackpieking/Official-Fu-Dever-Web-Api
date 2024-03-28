using System;
using FuDever.Domain.EntityBuilders.Skill.Others;

namespace FuDever.Domain.EntityBuilders.Skill;

/// <summary>
///     Skill for new record builder.
/// </summary>
public sealed class SkillForNewRecordBuilder :
    Entities.Skill,
    IBaseSkillBuilder,
    ISkillBuilder<SkillForNewRecordBuilder>
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

    public SkillForNewRecordBuilder WithId(Guid skillId)
    {
        // Validate skill Id.
        if (skillId == Guid.Empty)
        {
            return default;
        }

        Id = skillId;

        return this;
    }

    public SkillForNewRecordBuilder WithName(string skillName)
    {
        // Validate skill name.
        if (string.IsNullOrWhiteSpace(value: skillName) ||
            skillName.Length > Metadata.Name.MaxLength ||
            skillName.Length < Metadata.Name.MinLength)
        {
            return default;
        }

        Name = skillName;

        return this;
    }

    public SkillForNewRecordBuilder WithRemovedAt(DateTime skillRemovedAt)
    {
        // Validate skill removed at.
        if (skillRemovedAt == DateTime.MaxValue ||
            skillRemovedAt.Kind != DateTimeKind.Utc)
        {
            return default;
        }

        RemovedAt = skillRemovedAt;

        return this;
    }

    public SkillForNewRecordBuilder WithRemovedBy(Guid skillRemovedBy)
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
