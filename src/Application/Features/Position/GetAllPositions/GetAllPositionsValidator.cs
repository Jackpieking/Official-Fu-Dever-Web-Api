using FluentValidation;

namespace Application.Features.Position.GetAllPositions;

/// <summary>
///     Get all position request validator.
/// </summary>
public sealed class GetAllPositionsValidator : AbstractValidator<GetAllPositionsRequest>
{
    public GetAllPositionsValidator()
    {
        RuleFor(expression: request => request.CacheExpiredTime)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: cacheExpiredTime => cacheExpiredTime >= default(int));
    }
}
