using FluentValidation;
using System;

namespace FuDever.Application.Features.Position.RemovePositionTemporarilyByPositionId;

/// <summary>
///     Remove position temporarily by position id request validator.
/// </summary>
public sealed class RemovePositionTemporarilyByPositionIdRequestValidator :
    AbstractValidator<RemovePositionTemporarilyByPositionIdRequest>
{
    public RemovePositionTemporarilyByPositionIdRequestValidator()
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
