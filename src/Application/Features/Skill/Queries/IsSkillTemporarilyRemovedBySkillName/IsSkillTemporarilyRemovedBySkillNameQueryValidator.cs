using FluentValidation;

namespace Application.Features.Skill.Queries.IsSkillTemporarilyRemovedBySkillName;

/// <summary>
///     Is skill temporarily removed by skill name query modal validator.
/// </summary>
public sealed class IsSkillTemporarilyRemovedBySkillNameQueryValidator : AbstractValidator<IsSkillTemporarilyRemovedBySkillNameQuery>
{
    public IsSkillTemporarilyRemovedBySkillNameQueryValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: command => command.SkillName)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: skillName => !string.IsNullOrWhiteSpace(value: skillName))
            .Must(predicate: skillName => skillName.Length <=
                Domain.Entities.Skill.Metadata.Name.MaxLength);
    }
}
