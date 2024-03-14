using FluentValidation;
using System;

namespace FuDever.Application.Features.Skill.RestoreSkillBySkillId;

/// <summary>
///     Restore skill by skill id request validator.
/// </summary>
public sealed class RestoreSkillBySkillIdRequestValidator : AbstractValidator<RestoreSkillBySkillIdRequest>
{
    public RestoreSkillBySkillIdRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.SkillId)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: skillId => skillId != Guid.Empty);
    }
}
