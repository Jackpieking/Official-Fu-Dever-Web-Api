using FluentValidation;

namespace Application.Features.Skill.Commands.CreateSkill;

/// <summary>
///     Create skill command modal validator.
/// </summary>
internal sealed class CreateSkillCommandValidator : AbstractValidator<CreateSkillCommand>
{
    private const int MaxSkillNameLength = 100;

    internal CreateSkillCommandValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: command => command.NewSkillName)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: newSkillName => !string.IsNullOrWhiteSpace(value: newSkillName))
            .Must(predicate: newSkillName => newSkillName.Length <= MaxSkillNameLength);
    }
}
