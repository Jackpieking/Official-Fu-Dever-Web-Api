using System;
using FluentValidation;

namespace Application.Features.Skill.Commands.RestoreSkill;

/// <summary>
///     Restore skill command modal validator.
/// </summary>
internal sealed class RestoreSkillCommandValidator : AbstractValidator<RestoreSkillCommand>
{
    internal RestoreSkillCommandValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: command => command.SkillId)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: skillId => skillId != Guid.Empty);
    }
}
