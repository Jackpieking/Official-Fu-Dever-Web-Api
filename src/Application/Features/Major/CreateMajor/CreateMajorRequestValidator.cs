using FluentValidation;

namespace Application.Features.Major.CreateMajor;

/// <summary>
///     Create major request validator.
/// </summary>
public sealed class CreateMajorRequestValidator : AbstractValidator<CreateMajorRequest>
{
    public CreateMajorRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: command => command.NewMajorName)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: newMajorName =>
                !string.IsNullOrWhiteSpace(value: newMajorName))
            .Must(predicate: newMajorName => newMajorName.Length <=
                Domain.Entities.Major.Metadata.Name.MaxLength)
            .Must(predicate: newMajorName => newMajorName.Length >=
                Domain.Entities.Major.Metadata.Name.MinLength);
    }
}
