using FluentValidation;
using System;

namespace Application.Features.Skill.Commands.RemoveSkillPermanently;

/// <summary>
///     Remove skill permanently command modal validator.
/// </summary>
public sealed class RemoveSkillPermanentlyCommandValidator : AbstractValidator<RemoveSkillPermanentlyCommand>
{
    public RemoveSkillPermanentlyCommandValidator()
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
