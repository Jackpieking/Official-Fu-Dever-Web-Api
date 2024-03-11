using FluentValidation;
using System;

namespace Application.Features.Major.RemoveMajorPermanentlyByMajorId;

/// <summary>
///     Remove major permanently by major Id request validator.
/// </summary>
public sealed class RemoveMajorPermanentlyByMajorIdRequestValidator : AbstractValidator<RemoveMajorPermanentlyByMajorIdRequest>
{
    public RemoveMajorPermanentlyByMajorIdRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.MajorId)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: majorId => majorId != Guid.Empty);

        RuleFor(expression: request => request.MajorRemovedBy)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: majorRemovedBy => majorRemovedBy != Guid.Empty);
    }
}
