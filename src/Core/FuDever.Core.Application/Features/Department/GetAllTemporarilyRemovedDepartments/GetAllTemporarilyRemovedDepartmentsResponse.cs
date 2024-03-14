using System;
using System.Collections.Generic;

namespace FuDever.Application.Features.Department.GetAllTemporarilyRemovedDepartments;

/// <summary>
///     Get all temporarily removed departments response.
/// </summary>
public sealed class GetAllTemporarilyRemovedDepartmentsResponse
{
    public GetAllTemporarilyRemovedDepartmentsResponseStatusCode StatusCode { get; init; }

    public IEnumerable<Department> FoundTemporarilyRemovedDepartments { get; init; }

    public sealed class Department
    {
        public Guid DepartmentId { get; init; }

        public string DepartmentName { get; init; }

        public DateTime DepartmentRemovedAt { get; init; }

        public Guid DepartmentRemovedBy { get; init; }
    }
}
