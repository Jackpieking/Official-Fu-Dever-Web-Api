using FluentValidation;
using System;

namespace FuDever.Application.Features.Major.UpdateMajorByMajorId;

/// <summary>
///     Update major by major id request validator.
/// </summary>
public sealed class UpdateMajorByMajorIdRequestValidator : AbstractValidator<UpdateMajorByMajorIdRequest>
{
    public UpdateMajorByMajorIdRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.MajorId)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: majorId => majorId != Guid.Empty);

        RuleFor(expression: request => request.MajorUpdatedBy)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: majorUpdatedBy => majorUpdatedBy != Guid.Empty);

        RuleFor(expression: request => request.NewMajorName)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: newMajorName =>
                !string.IsNullOrWhiteSpace(value: newMajorName))
            .Must(predicate: newMajorName => newMajorName.Length <=
                Domain.Entities.Major.Metadata.Name.MaxLength)
            .Must(predicate: newMajorName => newMajorName.Length >=
                Domain.Entities.Major.Metadata.Name.MinLength);
    }
}
