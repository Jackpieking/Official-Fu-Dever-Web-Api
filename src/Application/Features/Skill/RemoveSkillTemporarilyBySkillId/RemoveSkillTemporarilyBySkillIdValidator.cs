using FluentValidation;
using System;

namespace Application.Features.Skill.RemoveSkillTemporarilyBySkillId;

/// <summary>
///     Remove skill temporarily by skill request validator.
/// </summary>
public sealed class RemoveSkillTemporarilyBySkillIdValidator :
    AbstractValidator<RemoveSkillTemporarilyBySkillIdRequest>
{
    public RemoveSkillTemporarilyBySkillIdValidator()
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
