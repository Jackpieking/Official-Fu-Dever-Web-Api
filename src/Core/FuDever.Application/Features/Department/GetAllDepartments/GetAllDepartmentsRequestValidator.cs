using FluentValidation;

namespace FuDever.Application.Features.Department.GetAllDepartments;

/// <summary>
///     Get all department request validator.
/// </summary>
public sealed class GetAllDepartmentsRequestValidator : AbstractValidator<GetAllDepartmentsRequest>
{
    public GetAllDepartmentsRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.CacheExpiredTime)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: cacheExpiredTime => cacheExpiredTime >= default(int));
    }
}
