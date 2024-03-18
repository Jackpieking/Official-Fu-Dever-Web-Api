using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Department;

namespace FuDever.PostgresSql.Specifications.Entities.Department;

/// <summary>
///     Represent implementation of department as no tracking specification.
/// </summary>
internal sealed class DepartmentAsNoTrackingSpecification :
    BaseSpecification<Domain.Entities.Department>,
    IDepartmentAsNoTrackingSpecification
{
    internal DepartmentAsNoTrackingSpecification()
    {
        IsAsNoTracking = true;
    }
}
