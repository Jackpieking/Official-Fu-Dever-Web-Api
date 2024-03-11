using FluentValidation;

namespace Application.Features.Platform.GetAllPlatformsByPlatformName;

/// <summary>
///     Get all platforms by name request validator.
/// </summary>
public sealed class GetAllPlatformsByPlatformNameRequestValidator : AbstractValidator<GetAllPlatformsByPlatformNameRequest>
{
    public GetAllPlatformsByPlatformNameRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.PlatformName)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: platformName =>
                !string.IsNullOrWhiteSpace(value: platformName))
            .Must(predicate: platformName => platformName.Length <=
                Domain.Entities.Platform.Metadata.Name.MaxLength)
            .Must(predicate: platformName => platformName.Length >=
                Domain.Entities.Platform.Metadata.Name.MinLength);

        RuleFor(expression: request => request.CacheExpiredTime)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: cacheExpiredTime => cacheExpiredTime >= default(int));
    }
}
