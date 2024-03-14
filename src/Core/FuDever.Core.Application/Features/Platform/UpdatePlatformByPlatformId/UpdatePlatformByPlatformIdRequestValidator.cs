using FluentValidation;
using System;

namespace FuDever.Application.Features.Platform.UpdatePlatformByPlatformId;

/// <summary>
///     Update platform by platform id request validator.
/// </summary>
public sealed class UpdatePlatformByPlatformIdRequestValidator : AbstractValidator<UpdatePlatformByPlatformIdRequest>
{
    public UpdatePlatformByPlatformIdRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.PlatformId)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: platformId => platformId != Guid.Empty);

        RuleFor(expression: request => request.PlatformUpdatedBy)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: platformUpdatedBy => platformUpdatedBy != Guid.Empty);

        RuleFor(expression: request => request.NewPlatformName)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: newPlatformName =>
                !string.IsNullOrWhiteSpace(value: newPlatformName))
            .Must(predicate: newSKillName => newSKillName.Length <=
                Domain.Entities.Platform.Metadata.Name.MaxLength)
            .Must(predicate: newPlatformName => newPlatformName.Length >=
                Domain.Entities.Platform.Metadata.Name.MinLength);
    }
}
