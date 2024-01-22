using FluentValidation;
using System;

namespace Application.Features.Skill.Commands.RemoveSkillTemporarily;

/// <summary>
///     Remove skill temporarily command modal validator.
/// </summary>
public sealed class RemoveSkillTemporarilyCommandValidator : AbstractValidator<RemoveSkillTemporarilyCommand>
{
    public RemoveSkillTemporarilyCommandValidator()
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
