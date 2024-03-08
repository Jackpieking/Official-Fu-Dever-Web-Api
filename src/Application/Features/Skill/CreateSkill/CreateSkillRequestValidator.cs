using FluentValidation;

namespace Application.Features.Skill.CreateSkill;

/// <summary>
///     Create skill request validator.
/// </summary>
public sealed class CreateSkillRequestValidator : AbstractValidator<CreateSkillRequest>
{
    public CreateSkillRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: command => command.NewSkillName)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: newSkillName =>
                !string.IsNullOrWhiteSpace(value: newSkillName))
            .Must(predicate: newSkillName => newSkillName.Length <=
                Domain.Entities.Skill.Metadata.Name.MaxLength)
            .Must(predicate: newSkillName => newSkillName.Length >=
                Domain.Entities.Skill.Metadata.Name.MinLength);
    }
}
