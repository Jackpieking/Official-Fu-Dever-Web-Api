using FuDever.Domain.Specifications.Base;
using System;

namespace FuDever.Domain.Specifications.Entities.Department;

/// <summary>
///     Represent update field of department specification.
/// </summary>
public interface IUpdateFieldOfDepartmentSpecification : IBaseSpecification<Domain.Entities.Department>
{
    IUpdateFieldOfDepartmentSpecification Ver1(
        DateTime departmentRemovedAt,
        Guid departmentRemovedBy);

    IUpdateFieldOfDepartmentSpecification Ver2(string departmentName);
}
