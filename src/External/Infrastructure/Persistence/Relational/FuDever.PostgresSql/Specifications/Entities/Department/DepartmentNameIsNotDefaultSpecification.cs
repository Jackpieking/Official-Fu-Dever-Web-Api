using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Department;

namespace FuDever.PostgresSql.Specifications.Entities.Department;

/// <summary>
///     Represent implementation of department name is not default specification.
/// </summary>
internal sealed class DepartmentNameIsNotDefaultSpecification :
    BaseSpecification<Domain.Entities.Department>,
    IDepartmentNameIsNotDefaultSpecification
{
    internal DepartmentNameIsNotDefaultSpecification()
    {
        WhereExpression = department => !department.Name.Equals(string.Empty);
    }
}
