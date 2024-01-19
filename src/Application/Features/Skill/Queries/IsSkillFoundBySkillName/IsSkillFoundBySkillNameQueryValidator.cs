using FluentValidation;

namespace Application.Features.Skill.Queries.IsSkillFoundBySkillName;

/// <summary>
///     Is skill found by skill name query modal validator.
/// </summary>
internal sealed class IsSkillFoundBySkillNameQueryValidator : AbstractValidator<IsSkillFoundBySkillNameQuery>
{
    private const int MaxSkillNameLength = 100;

    internal IsSkillFoundBySkillNameQueryValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: command => command.SkillName)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: skillName => !string.IsNullOrWhiteSpace(value: skillName))
            .Must(predicate: skillName => skillName.Length <= MaxSkillNameLength);
    }
}
