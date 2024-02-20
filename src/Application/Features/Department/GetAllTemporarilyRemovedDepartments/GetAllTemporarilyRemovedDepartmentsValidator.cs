using FluentValidation;

namespace Application.Features.Department.GetAllTemporarilyRemovedDepartments;

/// <summary>
///     Get all temporarily removed departments request validator.
/// </summary>
public sealed class GetAllTemporarilyRemovedDepartmentsValidator :
    AbstractValidator<GetAllTemporarilyRemovedDepartmentsRequest>
{
    public GetAllTemporarilyRemovedDepartmentsValidator()
    {
        RuleFor(expression: request => request.CacheExpiredTime)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: cacheExpiredTime => cacheExpiredTime >= default(int));
    }
}
