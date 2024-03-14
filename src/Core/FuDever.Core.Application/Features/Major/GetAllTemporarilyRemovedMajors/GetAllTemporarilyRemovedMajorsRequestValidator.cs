using FluentValidation;

namespace FuDever.Application.Features.Major.GetAllTemporarilyRemovedMajors;

/// <summary>
///     Get all temporarily removed majors request validator.
/// </summary>
public sealed class GetAllTemporarilyRemovedMajorsRequestValidator : AbstractValidator<GetAllTemporarilyRemovedMajorsRequest>
{
    public GetAllTemporarilyRemovedMajorsRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.CacheExpiredTime)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: cacheExpiredTime => cacheExpiredTime >= default(int));
    }
}
