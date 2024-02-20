using FluentValidation;

namespace Application.Features.Skill.GetAllSkillsBySkillName;

/// <summary>
///     Get all skills by name request validator.
/// </summary>
public sealed class GetAllSkillsBySkillNameValidator : AbstractValidator<GetAllSkillsBySkillNameRequest>
{
    public GetAllSkillsBySkillNameValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.SkillName)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: skillName =>
                !string.IsNullOrWhiteSpace(value: skillName))
            .Must(predicate: skillName => skillName.Length <=
                Domain.Entities.Skill.Metadata.Name.MaxLength)
            .Must(predicate: skillName => skillName.Length >=
                Domain.Entities.Skill.Metadata.Name.MinLength);

        RuleFor(expression: request => request.CacheExpiredTime)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: cacheExpiredTime => cacheExpiredTime >= default(int));
    }
}

