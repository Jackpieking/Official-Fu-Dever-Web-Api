using System;
using FluentValidation;

namespace Application.Features.Hobby.RemoveHobbyPermanentlyByHobbyId;

/// <summary>
///     Remove hobby permanently by hobby id request validator.
/// </summary>
public sealed class RemoveHobbyPermanentlyByHobbyIdRequestValidator : AbstractValidator<RemoveHobbyPermanentlyByHobbyIdRequest>
{
    public RemoveHobbyPermanentlyByHobbyIdRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.HobbyId)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: hobbyId => hobbyId != Guid.Empty);

        RuleFor(expression: request => request.HobbyRemovedBy)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: hobbyRemovedBy => hobbyRemovedBy != Guid.Empty);
    }
}
