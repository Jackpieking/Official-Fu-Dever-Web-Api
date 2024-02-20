using FluentValidation;

namespace Application.Features.Department.GetAllDepartments;

/// <summary>
///     Get all department request validator.
/// </summary>
public sealed class GetAllDepartmentsValidator : AbstractValidator<GetAllDepartmentsRequest>
{
    public GetAllDepartmentsValidator()
    {
        RuleFor(expression: request => request.CacheExpiredTime)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: cacheExpiredTime => cacheExpiredTime >= default(int));
    }
}
