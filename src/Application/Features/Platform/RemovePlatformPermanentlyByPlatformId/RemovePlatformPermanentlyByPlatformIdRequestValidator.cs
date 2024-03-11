using FluentValidation;
using System;

namespace Application.Features.Platform.RemovePlatformPermanentlyByPlatformId;

/// <summary>
///     Validator for remove platform permanently by platform id request.
/// </summary>
public sealed class RemovePlatformPermanentlyByPlatformIdRequestValidator : AbstractValidator<RemovePlatformPermanentlyByPlatformIdRequest>
{
    public RemovePlatformPermanentlyByPlatformIdRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.PlatformId)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: platformId => platformId != Guid.Empty);

        RuleFor(expression: request => request.PlatformRemovedBy)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: platformRemovedBy => platformRemovedBy != Guid.Empty);
    }
}
