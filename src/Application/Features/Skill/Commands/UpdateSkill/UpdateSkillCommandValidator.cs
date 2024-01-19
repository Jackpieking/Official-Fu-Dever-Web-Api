using System;
using FluentValidation;

namespace Application.Features.Skill.Commands.UpdateSkill;

/// <summary>
///     Update skill command modal validator.
/// </summary>
internal sealed class UpdateSkillCommandValidator : AbstractValidator<UpdateSkillCommand>
{
    private const int MaxSkillNameLength = 100;

    internal UpdateSkillCommandValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: command => command.SkillId)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: skillId => skillId != Guid.Empty);

        RuleFor(expression: command => command.SkillUpdatedBy)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: skillUpdatedBy => skillUpdatedBy != Guid.Empty);

        RuleFor(expression: command => command.NewSkillName)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: newSkillName => !string.IsNullOrWhiteSpace(value: newSkillName))
            .Must(predicate: newSkillName => newSkillName.Length <= MaxSkillNameLength);
    }
}
