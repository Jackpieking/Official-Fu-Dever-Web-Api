using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Skill;
using System;

namespace FuDever.PostgresSql.Specifications.Entities.Skill;

/// <summary>
///     Represent implementation of update
///     field of skill specification.
/// </summary>
internal sealed class UpdateFieldOfSkillSpecification :
    BaseSpecification<Domain.Entities.Skill>,
    IUpdateFieldOfSkillSpecification
{
    public IUpdateFieldOfSkillSpecification Ver1(
        DateTime skillRemovedAt,
        Guid skillRemovedBy)
    {
        // Validate skill removed time.
        if (skillRemovedAt == DateTime.MaxValue)
        {
            return default;
        }

        // Validate skill remover.
        if (skillRemovedBy == Guid.Empty)
        {
            return default;
        }

        UpdateExpression = setPropertyCall => setPropertyCall;

        UpdateExpression = AppendSetProperty(
            left: UpdateExpression,
            right: setPropertyCall => setPropertyCall
                .SetProperty(
                    skill => skill.RemovedAt,
                    skillRemovedAt)
                .SetProperty(
                    skill => skill.RemovedBy,
                    skillRemovedBy));

        return this;
    }

    public IUpdateFieldOfSkillSpecification Ver2(string skillName)
    {
        // Validate skill name.
        if (string.IsNullOrWhiteSpace(value: skillName) ||
            skillName.Length > Domain.Entities.Skill.Metadata.Name.MaxLength ||
            skillName.Length < Domain.Entities.Skill.Metadata.Name.MinLength)
        {
            return default;
        }

        UpdateExpression = setPropertyCall => setPropertyCall;

        UpdateExpression = AppendSetProperty(
            left: UpdateExpression,
            right: setPropertyCall => setPropertyCall.SetProperty(
                skill => skill.Name,
                skillName));

        return this;
    }
}
