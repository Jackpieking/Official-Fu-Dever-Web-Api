using FluentValidation;

namespace FuDever.Application.Features.Skill.GetAllSkills;

/// <summary>
///     Get all skills request validator.
/// </summary>
public sealed class GetAllSkillsRequestValidator : AbstractValidator<GetAllSkillsRequest>
{
    public GetAllSkillsRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.CacheExpiredTime)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: cacheExpiredTime => cacheExpiredTime >= default(int));
    }
}
