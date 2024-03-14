using System;
using System.Collections.Generic;

namespace FuDever.Application.Features.Department.GetAllDepartments;

/// <summary>
///     Get all department response.
/// </summary>
public sealed class GetAllDepartmentsResponse
{
    public GetAllDepartmentsResponseStatusCode StatusCode { get; init; }

    public IEnumerable<Department> FoundDepartments { get; init; }

    public sealed class Department
    {
        public Guid DepartmentId { get; init; }

        public string DepartmentName { get; init; }
    }
}
