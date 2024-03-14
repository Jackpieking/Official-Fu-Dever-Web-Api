using FluentValidation;

namespace FuDever.Application.Features.Position.GetAllPositions;

/// <summary>
///     Get all position request validator.
/// </summary>
public sealed class GetAllPositionsRequestValidator : AbstractValidator<GetAllPositionsRequest>
{
    public GetAllPositionsRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.CacheExpiredTime)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: cacheExpiredTime => cacheExpiredTime >= default(int));
    }
}
