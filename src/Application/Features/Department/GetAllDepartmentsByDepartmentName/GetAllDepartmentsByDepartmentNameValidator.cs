using FluentValidation;

namespace Application.Features.Department.GetAllDepartmentsByDepartmentName;

/// <summary>
///     Get all departments by department name request validator.
/// </summary>
public sealed class GetAllDepartmentsByDepartmentNameValidator : AbstractValidator<GetAllDepartmentsByDepartmentNameRequest>
{
    public GetAllDepartmentsByDepartmentNameValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.DepartmentName)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: departmentName =>
                !string.IsNullOrWhiteSpace(value: departmentName))
            .Must(predicate: departmentNam => departmentNam.Length <=
                Domain.Entities.Department.Metadata.Name.MaxLength)
            .Must(predicate: departmentNam => departmentNam.Length >=
                Domain.Entities.Department.Metadata.Name.MinLength);

        RuleFor(expression: request => request.CacheExpiredTime)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: cacheExpiredTime => cacheExpiredTime >= default(int));
    }
}
