using System;
using FluentValidation;

namespace Application.Features.Department.UpdateDepartmentByDepartmentId;

/// <summary>
///     Update department by department id request validator.
/// </summary>
public sealed class UpdateDepartmentByDepartmentIdValidator :
    AbstractValidator<UpdateDepartmentByDepartmentIdRequest>
{
    public UpdateDepartmentByDepartmentIdValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.DepartmentId)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: departmentId => departmentId != Guid.Empty);

        RuleFor(expression: request => request.DepartmentUpdatedBy)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: departmentUpdatedBy => departmentUpdatedBy != Guid.Empty);

        RuleFor(expression: request => request.NewDepartmentName)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: newDepartmentName =>
                !string.IsNullOrWhiteSpace(value: newDepartmentName))
            .Must(predicate: newDepartmentName => newDepartmentName.Length <=
                Domain.Entities.Department.Metadata.Name.MaxLength)
            .Must(predicate: newDepartmentName => newDepartmentName.Length >=
                Domain.Entities.Department.Metadata.Name.MinLength);
    }
}
