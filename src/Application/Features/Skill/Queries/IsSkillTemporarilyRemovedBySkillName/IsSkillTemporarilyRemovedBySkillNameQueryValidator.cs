using FluentValidation;

namespace Application.Features.Skill.Queries.IsSkillTemporarilyRemovedBySkillName;

/// <summary>
///     Is skill temporarily removed by skill name query modal validator.
/// </summary>
internal sealed class IsSkillTemporarilyRemovedBySkillNameQueryValidator : AbstractValidator<IsSkillTemporarilyRemovedBySkillNameQuery>
{
    private const int MaxSkillNameLength = 100;

    internal IsSkillTemporarilyRemovedBySkillNameQueryValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: command => command.SkillName)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: skillName => !string.IsNullOrWhiteSpace(value: skillName))
            .Must(predicate: skillName => skillName.Length <= MaxSkillNameLength);
    }
}
