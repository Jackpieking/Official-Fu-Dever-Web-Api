using FluentValidation;

namespace FuDever.Application.Features.Position.CreatePosition;

/// <summary>
///     Create position request validator.
/// </summary>
public sealed class CreatePositionRequestValidator : AbstractValidator<CreatePositionRequest>
{
    public CreatePositionRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: command => command.NewPositionName)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: newPositionName =>
                !string.IsNullOrWhiteSpace(value: newPositionName))
            .Must(predicate: newPositionName => newPositionName.Length <=
                Domain.Entities.Position.Metadata.Name.MaxLength)
            .Must(predicate: newPositionName => newPositionName.Length >=
                Domain.Entities.Position.Metadata.Name.MinLength);
    }
}
