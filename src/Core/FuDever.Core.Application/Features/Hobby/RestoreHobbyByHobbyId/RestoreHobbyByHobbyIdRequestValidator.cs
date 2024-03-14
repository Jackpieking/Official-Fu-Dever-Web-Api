using FluentValidation;
using System;

namespace FuDever.Application.Features.Hobby.RestoreHobbyByHobbyId;

/// <summary>
///     Restore hobby by hobby id request validator.
/// </summary>
public sealed class RestoreHobbyByHobbyIdRequestValidator : AbstractValidator<RestoreHobbyByHobbyIdRequest>
{
    public RestoreHobbyByHobbyIdRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.HobbyId)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: skillId => skillId != Guid.Empty);
    }
}
