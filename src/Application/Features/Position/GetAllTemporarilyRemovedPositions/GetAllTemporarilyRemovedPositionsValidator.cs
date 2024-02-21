using FluentValidation;

namespace Application.Features.Position.GetAllTemporarilyRemovedPositions;

/// <summary>
///     Get all temporarily removed positions request validator.
/// </summary>
public sealed class GetAllTemporarilyRemovedPositionsValidator :
    AbstractValidator<GetAllTemporarilyRemovedPositionsRequest>
{
    public GetAllTemporarilyRemovedPositionsValidator()
    {
        RuleFor(expression: request => request.CacheExpiredTime)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: cacheExpiredTime => cacheExpiredTime >= default(int));
    }
}
