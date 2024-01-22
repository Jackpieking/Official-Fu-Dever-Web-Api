using FluentValidation;
using System;

namespace Application.Features.Skill.Queries.IsSkillTemporarilyRemovedBySkillId;

/// <summary>
///     Is skill temporarily removed by skill id query modal validator.
/// </summary>
public sealed class IsSkillTemporarilyRemovedBySkillIdQueryValidator : AbstractValidator<IsSkillTemporarilyRemovedBySkillIdQuery>
{
    public IsSkillTemporarilyRemovedBySkillIdQueryValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: command => command.SkillId)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: skillId => skillId != Guid.Empty);
    }
}
