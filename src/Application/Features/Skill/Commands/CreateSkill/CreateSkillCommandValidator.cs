using FluentValidation;

namespace Application.Features.Skill.Commands.CreateSkill;

/// <summary>
///     Create skill command modal validator.
/// </summary>
public sealed class CreateSkillCommandValidator : AbstractValidator<CreateSkillCommand>
{
    public CreateSkillCommandValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: command => command.NewSkillName)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: newSkillName => !string.IsNullOrWhiteSpace(value: newSkillName))
            .Must(predicate: newSkillName => newSkillName.Length <=
                Domain.Entities.Skill.Metadata.Name.MaxLength);
    }
}
