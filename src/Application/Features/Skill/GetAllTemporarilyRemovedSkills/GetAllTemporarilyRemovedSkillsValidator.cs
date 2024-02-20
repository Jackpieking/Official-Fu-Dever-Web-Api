using FluentValidation;

namespace Application.Features.Skill.GetAllTemporarilyRemovedSkills;

/// <summary>
///     Get all temporarily removed skills request validator.
/// </summary>
public sealed class GetAllTemporarilyRemovedSkillsValidator :
    AbstractValidator<GetAllTemporarilyRemovedSkillsRequest>
{
    public GetAllTemporarilyRemovedSkillsValidator()
    {
        RuleFor(expression: request => request.CacheExpiredTime)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: cacheExpiredTime => cacheExpiredTime >= default(int));
    }
}
