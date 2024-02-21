using FluentValidation;
using System;

namespace Application.Features.Position.RemovePositionPermanentlyByPositionId;

/// <summary>
///     Remove position permanently by position id request validator.
/// </summary>
public sealed class RemovePositionPermanentlyByPositionIdValidator :
    AbstractValidator<RemovePositionPermanentlyByPositionIdRequest>
{
    public RemovePositionPermanentlyByPositionIdValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.PositionId)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: positionId => positionId != Guid.Empty);

        RuleFor(expression: request => request.PositionRemovedBy)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: positionRemovedBy => positionRemovedBy != Guid.Empty);
    }
}
