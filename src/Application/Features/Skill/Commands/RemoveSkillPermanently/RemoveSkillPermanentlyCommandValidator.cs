using System;
using FluentValidation;

namespace Application.Features.Skill.Commands.RemoveSkillPermanently;

/// <summary>
///     Remove skill permanently command modal validator.
/// </summary>
internal sealed class RemoveSkillPermanentlyCommandValidator : AbstractValidator<RemoveSkillPermanentlyCommand>
{
    internal RemoveSkillPermanentlyCommandValidator()
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
