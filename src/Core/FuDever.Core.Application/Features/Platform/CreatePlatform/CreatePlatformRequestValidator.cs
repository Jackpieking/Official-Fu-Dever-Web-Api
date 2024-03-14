using FluentValidation;

namespace FuDever.Application.Features.Platform.CreatePlatform;

/// <summary>
///     Create platform request validator.
/// </summary>
public sealed class CreatePlatformRequestValidator : AbstractValidator<CreatePlatformRequest>
{
    public CreatePlatformRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: command => command.NewPlatformName)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: newPlatformName =>
                !string.IsNullOrWhiteSpace(value: newPlatformName))
            .Must(predicate: newPlatformName => newPlatformName.Length <=
                Domain.Entities.Platform.Metadata.Name.MaxLength)
            .Must(predicate: newPlatformName => newPlatformName.Length >=
                Domain.Entities.Platform.Metadata.Name.MinLength);
    }
}
