using FluentValidation;
using System;

namespace FuDever.Application.Features.Major.RestoreMajorByMajorId;

/// <summary>
///     Restore major by major id request handler.
/// </summary>
public sealed class RestoreMajorByMajorIdRequestValidator : AbstractValidator<RestoreMajorByMajorIdRequest>
{
    public RestoreMajorByMajorIdRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.MajorId)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: departmentId => departmentId != Guid.Empty);
    }
}
