using FluentValidation;
using System;

namespace Application.Features.Position.UpdatePosition;

/// <summary>
///     Update position by position id request validator.
/// </summary>
public sealed class UpdatePositionByPositionIdValidator :
    AbstractValidator<UpdatePositionByPositionIdRequest>
{
    public UpdatePositionByPositionIdValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.PositionId)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: positionId => positionId != Guid.Empty);

        RuleFor(expression: request => request.PositionUpdatedBy)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: positionUpdatedBy => positionUpdatedBy != Guid.Empty);

        RuleFor(expression: request => request.NewPositionName)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: newPositionName =>
                !string.IsNullOrWhiteSpace(value: newPositionName))
            .Must(predicate: newPositionName => newPositionName.Length <=
                Domain.Entities.Position.Metadata.Name.MaxLength)
            .Must(predicate: newPositionName => newPositionName.Length >=
                Domain.Entities.Position.Metadata.Name.MinLength);
    }
}
