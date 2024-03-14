using FluentValidation;

namespace FuDever.Application.Features.Position.GetAllTemporarilyRemovedPositions;

/// <summary>
///     Get all temporarily removed positions request validator.
/// </summary>
public sealed class GetAllTemporarilyRemovedPositionsRequestValidator :
    AbstractValidator<GetAllTemporarilyRemovedPositionsRequest>
{
    public GetAllTemporarilyRemovedPositionsRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.CacheExpiredTime)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: cacheExpiredTime => cacheExpiredTime >= default(int));
    }
}
