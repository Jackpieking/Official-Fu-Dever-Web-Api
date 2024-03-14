using System;
using System.Collections.Generic;

namespace FuDever.Application.Features.Department.GetAllDepartmentsByDepartmentName;

/// <summary>
///     Get all departments by department name response.
/// </summary>
public sealed class GetAllDepartmentsByDepartmentNameResponse
{
    public GetAllDepartmentsByDepartmentNameResponseStatusCode StatusCode { get; init; }

    public IEnumerable<Department> FoundDepartments { get; init; }

    public sealed class Department
    {
        public Guid DepartmentId { get; init; }

        public string DepartmentName { get; init; }
    }
}
