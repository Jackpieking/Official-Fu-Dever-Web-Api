using FluentValidation;
using System;

namespace Application.Features.Skill.Queries.IsSkillFoundBySkillId;

/// <summary>
///     Is skill found by skill id query modal validator.
/// </summary>
public sealed class IsSkillFoundBySkillIdQueryValidator : AbstractValidator<IsSkillFoundBySkillIdQuery>
{
    public IsSkillFoundBySkillIdQueryValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: command => command.SkillId)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: skillId => skillId != Guid.Empty);
    }
}
