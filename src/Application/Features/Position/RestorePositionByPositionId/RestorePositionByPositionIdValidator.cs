using FluentValidation;
using System;

namespace Application.Features.Position.RestorePositionByPositionId;

/// <summary>
///     Restore position by position id request validator.
/// </summary>
public sealed class RestorePositionByPositionIdValidator :
    AbstractValidator<RestorePositionByPositionIdRequest>
{
    public RestorePositionByPositionIdValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.PositionId)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: positionId => positionId != Guid.Empty);
    }
}
