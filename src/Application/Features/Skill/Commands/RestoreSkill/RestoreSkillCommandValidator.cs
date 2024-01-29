using FluentValidation;
using System;

namespace Application.Features.Skill.Commands.RestoreSkill;

/// <summary>
///     Restore skill command model validator.
/// </summary>
public sealed class RestoreSkillCommandValidator : AbstractValidator<RestoreSkillCommand>
{
    public RestoreSkillCommandValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: command => command.SkillId)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: skillId => skillId != Guid.Empty);
    }
}
