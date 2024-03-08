using FluentValidation;
using System;

namespace Application.Features.Department.RemoveDepartmentPermanentlyByDepartmentId;

/// <summary>
///     Remove department permanently by department id request validator.
/// </summary>
public sealed class RemoveDepartmentPermanentlyByDepartmentIdRequestValidator :
    AbstractValidator<RemoveDepartmentPermanentlyByDepartmentIdRequest>
{
    public RemoveDepartmentPermanentlyByDepartmentIdRequestValidator()
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
