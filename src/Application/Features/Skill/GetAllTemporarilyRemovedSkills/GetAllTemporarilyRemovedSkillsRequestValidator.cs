using FluentValidation;

namespace Application.Features.Skill.GetAllTemporarilyRemovedSkills;

/// <summary>
///     Get all temporarily removed skills request validator.
/// </summary>
public sealed class GetAllTemporarilyRemovedSkillsRequestValidator :
    AbstractValidator<GetAllTemporarilyRemovedSkillsRequest>
{
    public GetAllTemporarilyRemovedSkillsRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.CacheExpiredTime)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: cacheExpiredTime => cacheExpiredTime >= default(int));
    }
}
