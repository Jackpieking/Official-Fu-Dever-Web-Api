using FluentValidation;

namespace FuDever.Application.Features.Platform.GetAllPlatforms;

/// <summary>
///     Get all platforms request validator.
/// </summary>
public sealed class GetAllPlatformsRequestValidator : AbstractValidator<GetAllPlatformsRequest>
{
    public GetAllPlatformsRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.CacheExpiredTime)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: cacheExpiredTime => cacheExpiredTime >= default(int));
    }
}
