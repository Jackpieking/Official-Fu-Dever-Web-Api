using FluentValidation;
using System;

namespace Application.Features.Skill.Queries.IsSkillTemporarilyRemovedBySkillId;

/// <summary>
///     Is skill temporarily removed by skill id query model validator.
/// </summary>
public sealed class IsSkillTemporarilyRemovedBySkillIdQueryValidator : AbstractValidator<IsSkillTemporarilyRemovedBySkillIdQuery>
{
    public IsSkillTemporarilyRemovedBySkillIdQueryValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: query => query.SkillId)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: skillId => skillId != Guid.Empty);

        RuleFor(expression: query => query.CacheExpiredTime)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: cacheExpiredTime => cacheExpiredTime >= default(int));
    }
}
