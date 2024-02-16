using FluentValidation;

namespace Application.Features.Skill.GetAllSkills;

/// <summary>
///     Get all skills request validator.
/// </summary>
public sealed class GetAllSkillsValidator : AbstractValidator<GetAllSkillsRequest>
{
    public GetAllSkillsValidator()
    {
        RuleFor(expression: request => request.CacheExpiredTime)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: cacheExpiredTime => cacheExpiredTime >= default(int));
    }
}
