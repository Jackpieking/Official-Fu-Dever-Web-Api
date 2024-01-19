using System;
using FluentValidation;

namespace Application.Features.Skill.Queries.IsSkillTemporarilyRemovedBySkillId;

/// <summary>
///     Is skill temporarily removed by skill id query modal validator.
/// </summary>
internal sealed class IsSkillTemporarilyRemovedBySkillIdQueryValidator : AbstractValidator<IsSkillTemporarilyRemovedBySkillIdQuery>
{
    internal IsSkillTemporarilyRemovedBySkillIdQueryValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: command => command.SkillId)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: skillId => skillId != Guid.Empty);
    }
}
