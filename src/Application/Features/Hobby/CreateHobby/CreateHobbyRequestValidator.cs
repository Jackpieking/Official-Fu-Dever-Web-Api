using FluentValidation;

namespace Application.Features.Hobby.CreateHobby;

/// <summary>
///     Validator for create hobby request.
/// </summary>
public sealed class CreateHobbyRequestValidator : AbstractValidator<CreateHobbyRequest>
{
    public CreateHobbyRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: command => command.NewHobbyName)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: newHobbyName =>
                !string.IsNullOrWhiteSpace(value: newHobbyName))
            .Must(predicate: newHobbyName => newHobbyName.Length <=
                Domain.Entities.Hobby.Metadata.Name.MaxLength)
            .Must(predicate: newHobbyName => newHobbyName.Length >=
                Domain.Entities.Hobby.Metadata.Name.MinLength);
    }
}
