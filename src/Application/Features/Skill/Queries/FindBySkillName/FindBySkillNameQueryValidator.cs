using FluentValidation;

namespace Application.Features.Skill.Queries.FindBySkillName;

/// <summary>
///     Find by skill name query modal validator.
/// </summary>
public sealed class FindBySkillNameQueryValidator : AbstractValidator<FindBySkillNameQuery>
{
    public FindBySkillNameQueryValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: command => command.SkillName)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: skillName => !string.IsNullOrWhiteSpace(value: skillName))
            .Must(predicate: skillName => skillName.Length <=
                Domain.Entities.Skill.Metadata.Name.MaxLength);
    }
}
