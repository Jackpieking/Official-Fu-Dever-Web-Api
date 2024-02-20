using System;
using FluentValidation;

namespace Application.Features.Department.RestoreDepartmentByDepartmentId;

/// <summary>
///     Restore department by department id request validator.
/// </summary>
public sealed class RestoreDepartmentByDepartmentIdValidator :
    AbstractValidator<RestoreDepartmentByDepartmentIdRequest>
{
    public RestoreDepartmentByDepartmentIdValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.DepartmentId)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: departmentId => departmentId != Guid.Empty);
    }
}
