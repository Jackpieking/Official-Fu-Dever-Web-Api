using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Department;
using System;

namespace FuDever.PostgresSql.Specifications.Entities.Department;

/// <summary>
///     Represent implementation of update field of department specification.
/// </summary>
internal sealed class UpdateFieldOfDepartmentSpecification :
    BaseSpecification<Domain.Entities.Department>,
    IUpdateFieldOfDepartmentSpecification
{
    public IUpdateFieldOfDepartmentSpecification Ver1(
        DateTime departmentRemovedAt,
        Guid departmentRemovedBy)
    {
        // Validate department removed time.
        if (departmentRemovedAt == DateTime.MaxValue)
        {
            return default;
        }

        // Validate department remover.
        if (departmentRemovedBy == Guid.Empty)
        {
            return default;
        }

        UpdateExpression = setPropertyCall => setPropertyCall
            .SetProperty(
                department => department.RemovedAt,
                departmentRemovedAt)
            .SetProperty(
                department => department.RemovedBy,
                departmentRemovedBy);

        return this;
    }

    public IUpdateFieldOfDepartmentSpecification Ver2(string departmentName)
    {
        // Validate department name.
        if (string.IsNullOrWhiteSpace(value: departmentName) ||
            departmentName.Length > Domain.Entities.Department.Metadata.Name.MaxLength ||
            departmentName.Length < Domain.Entities.Department.Metadata.Name.MinLength)
        {
            return default;
        }

        UpdateExpression = setPropertyCall => setPropertyCall.SetProperty(
            department => department.Name,
            departmentName);

        return this;
    }
}
