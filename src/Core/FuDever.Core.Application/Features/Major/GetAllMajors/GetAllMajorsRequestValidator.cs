using FluentValidation;

namespace FuDever.Application.Features.Major.GetAllMajors;

/// <summary>
///     Get all majors request validator.
/// </summary>
public sealed class GetAllMajorsRequestValidator : AbstractValidator<GetAllMajorsRequest>
{
    public GetAllMajorsRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.CacheExpiredTime)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: cacheExpiredTime => cacheExpiredTime >= default(int));
    }
}
