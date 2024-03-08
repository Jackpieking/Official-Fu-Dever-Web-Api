using FluentValidation;
using System;

namespace Application.Features.Department.RestoreDepartmentByDepartmentId;

/// <summary>
///     Restore department by department id request validator.
/// </summary>
public sealed class RestoreDepartmentByDepartmentIdRequestValidator :
    AbstractValidator<RestoreDepartmentByDepartmentIdRequest>
{
    public RestoreDepartmentByDepartmentIdRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.DepartmentId)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: departmentId => departmentId != Guid.Empty);
    }
}
