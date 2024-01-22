using Domain.Specifications.Base;
using Domain.Specifications.Entities.Department;

namespace Persistence.SqlServer2016.Specifications.Entities.Department;

/// <summary>
///     Represent implementation of select fields from the "Departments"
///     table specification.
/// </summary>
internal sealed class SelectFieldsFromDepartmentSpecification :
    BaseSpecification<Domain.Entities.Department>,
    ISelectFieldsFromDepartmentSpecification
{
    public ISelectFieldsFromDepartmentSpecification Ver1()
    {
        SelectExpression = department => Domain.Entities.Department.Init(
            department.Id,
            department.Name);

        return this;
    }

    public ISelectFieldsFromDepartmentSpecification Ver2()
    {
        SelectExpression = department => Domain.Entities.Department.Init(
            department.Id,
            department.Name,
            department.RemovedAt,
            department.RemovedBy);

        return this;
    }
}
