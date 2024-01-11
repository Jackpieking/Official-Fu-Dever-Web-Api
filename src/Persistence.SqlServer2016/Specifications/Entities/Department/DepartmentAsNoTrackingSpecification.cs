using Domain.Specifications.Base;
using Domain.Specifications.Entities.Department;

namespace Persistence.SqlServer2016.Specifications.Entities.Department;

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
