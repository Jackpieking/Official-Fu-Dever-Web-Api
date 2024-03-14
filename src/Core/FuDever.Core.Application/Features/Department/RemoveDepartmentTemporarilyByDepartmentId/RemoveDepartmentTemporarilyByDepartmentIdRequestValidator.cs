using FluentValidation;
using System;

namespace FuDever.Application.Features.Department.RemoveDepartmentTemporarilyByDepartmentId;

/// <summary>
///     Remove department temporarily by department id request validator.
/// </summary>
public sealed class RemoveDepartmentTemporarilyByDepartmentIdRequestValidator :
    AbstractValidator<RemoveDepartmentTemporarilyByDepartmentIdRequest>
{
    public RemoveDepartmentTemporarilyByDepartmentIdRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.DepartmentId)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: departmentId => departmentId != Guid.Empty);

        RuleFor(expression: request => request.DepartmentRemovedBy)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: departmentRemovedBy => departmentRemovedBy != Guid.Empty);
    }
}
