using FluentValidation;
using System;

namespace FuDever.Application.Features.Skill.RemoveSkillTemporarilyBySkillId;

/// <summary>
///     Remove skill temporarily by skill id request validator.
/// </summary>
public sealed class RemoveSkillTemporarilyBySkillIdRequestValidator :
    AbstractValidator<RemoveSkillTemporarilyBySkillIdRequest>
{
    public RemoveSkillTemporarilyBySkillIdRequestValidator()
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
