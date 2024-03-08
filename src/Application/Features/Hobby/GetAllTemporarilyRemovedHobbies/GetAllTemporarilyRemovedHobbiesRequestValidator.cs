using FluentValidation;

namespace Application.Features.Hobby.GetAllTemporarilyRemovedHobbies;

/// <summary>
///     Get all temporarily removed hobbies request validator.
/// </summary>
public sealed class GetAllTemporarilyRemovedHobbiesRequestValidator : AbstractValidator<GetAllTemporarilyRemovedHobbiesRequest>
{
    public GetAllTemporarilyRemovedHobbiesRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.CacheExpiredTime)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: cacheExpiredTime => cacheExpiredTime >= default(int));
    }
}
