using FluentValidation;
using System;

namespace Application.Features.Skill.RemoveSkillPermanentlyBySkillId;

/// <summary>
///     Remove skill permanently by skill id request validator.
/// </summary>
public sealed class RemoveSkillPermanentlyBySkillIdRequestValidator :
    AbstractValidator<RemoveSkillPermanentlyBySkillIdRequest>
{
    public RemoveSkillPermanentlyBySkillIdRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.SkillId)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: skillId => skillId != Guid.Empty);

        RuleFor(expression: request => request.SkillRemovedBy)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: skillRemovedBy => skillRemovedBy != Guid.Empty);
    }
}
