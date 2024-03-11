using FluentValidation;

namespace Application.Features.Platform.GetAllTemporarilyRemovedPlatforms;

/// <summary>
///     Get all temporarily removed platforms request validator.
/// </summary>
public sealed class GetAllTemporarilyRemovedPlatformsRequestValidator : AbstractValidator<GetAllTemporarilyRemovedPlatformsRequest>
{
    public GetAllTemporarilyRemovedPlatformsRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.CacheExpiredTime)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: cacheExpiredTime => cacheExpiredTime >= default(int));
    }
}
