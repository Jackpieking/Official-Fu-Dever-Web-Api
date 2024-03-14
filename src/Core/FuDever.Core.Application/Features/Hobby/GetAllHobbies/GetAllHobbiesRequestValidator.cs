using FluentValidation;

namespace FuDever.Application.Features.Hobby.GetAllHobbies;

/// <summary>
///     Get all hobbies request validator.
/// </summary>
public sealed class GetAllHobbiesRequestValidator : AbstractValidator<GetAllHobbiesRequest>
{
    public GetAllHobbiesRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.CacheExpiredTime)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: cacheExpiredTime => cacheExpiredTime >= default(int));
    }
}
