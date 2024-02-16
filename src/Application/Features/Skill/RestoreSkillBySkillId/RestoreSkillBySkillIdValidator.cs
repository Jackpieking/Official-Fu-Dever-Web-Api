using System;
using FluentValidation;

namespace Application.Features.Skill.RestoreSkillBySkillId;

/// <summary>
///     Restore skill by skill id request validator.
/// </summary>
public sealed class RestoreSkillBySkillIdValidator : AbstractValidator<RestoreSkillBySkillIdRequest>
{
    public RestoreSkillBySkillIdValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.SkillId)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: skillId => skillId != Guid.Empty);
    }
}
