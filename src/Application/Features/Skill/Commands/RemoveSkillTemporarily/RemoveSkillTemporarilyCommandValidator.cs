using System;
using FluentValidation;

namespace Application.Features.Skill.Commands.RemoveSkillTemporarily;

/// <summary>
///     Remove skill temporarily command modal validator.
/// </summary>
internal sealed class RemoveSkillTemporarilyCommandValidator : AbstractValidator<RemoveSkillTemporarilyCommand>
{
    internal RemoveSkillTemporarilyCommandValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: command => command.SkillId)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: skillId => skillId != Guid.Empty);

        RuleFor(expression: command => command.SkillRemovedBy)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: skillRemovedBy => skillRemovedBy != Guid.Empty);
    }
}
