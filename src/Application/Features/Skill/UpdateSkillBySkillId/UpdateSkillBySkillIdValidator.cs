using System;
using FluentValidation;

namespace Application.Features.Skill.UpdateSkillBySkillId;

/// <summary>
///     Update skill by skill id request validator.
/// </summary>
public sealed class UpdateSkillBySkillIdValidator : AbstractValidator<UpdateSkillBySkillIdRequest>
{
    public UpdateSkillBySkillIdValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.SkillId)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: skillId => skillId != Guid.Empty);

        RuleFor(expression: request => request.SkillUpdatedBy)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: skillUpdatedBy => skillUpdatedBy != Guid.Empty);

        RuleFor(expression: request => request.NewSkillName)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: newSkillName => !string.IsNullOrWhiteSpace(value: newSkillName))
            .Must(predicate: newSKillName => newSKillName.Length <=
                Domain.Entities.Skill.Metadata.Name.MaxLength)
            .Must(predicate: newSkillName => newSkillName.Length >=
                Domain.Entities.Skill.Metadata.Name.MinLength);
    }
}
