using FluentValidation;
using System;

namespace FuDever.Application.Features.Hobby.UpdateHobbyByHobbyId;

/// <summary>
///     Update hobby by hobby id request validator.
/// </summary>
public sealed class UpdateHobbyByHobbyIdRequestValidator : AbstractValidator<UpdateHobbyByHobbyIdRequest>
{
    public UpdateHobbyByHobbyIdRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.HobbyId)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: hobbyId => hobbyId != Guid.Empty);

        RuleFor(expression: request => request.HobbyUpdatedBy)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: hobbyUpdatedBy => hobbyUpdatedBy != Guid.Empty);

        RuleFor(expression: request => request.NewHobbyName)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: newHobbyName =>
                !string.IsNullOrWhiteSpace(value: newHobbyName))
            .Must(predicate: newHobbyName => newHobbyName.Length <=
                Domain.Entities.Hobby.Metadata.Name.MaxLength)
            .Must(predicate: newHobbyName => newHobbyName.Length >=
                Domain.Entities.Hobby.Metadata.Name.MinLength);
    }
}
