using FluentValidation;

namespace Application.Features.Skill.Queries.IsSkillFoundBySkillName;

/// <summary>
///     Is skill found by skill name query model validator.
/// </summary>
public sealed class IsSkillFoundBySkillNameQueryValidator : AbstractValidator<IsSkillFoundBySkillNameQuery>
{
    public IsSkillFoundBySkillNameQueryValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: query => query.SkillName)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: skillName => !string.IsNullOrWhiteSpace(value: skillName))
            .Must(predicate: skillName => skillName.Length <=
                Domain.Entities.Skill.Metadata.Name.MaxLength);

        RuleFor(expression: query => query.CacheExpiredTime)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: cacheExpiredTime => cacheExpiredTime >= default(int));
    }
}
