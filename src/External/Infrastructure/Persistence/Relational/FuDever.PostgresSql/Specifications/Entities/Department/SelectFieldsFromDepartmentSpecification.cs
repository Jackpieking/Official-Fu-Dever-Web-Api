using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Department;

namespace FuDever.PostgresSql.Specifications.Entities.Department;

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
        SelectExpression = department => Domain.Entities.Department.InitFromDatabaseVer1(
            department.Id,
            department.Name);

        return this;
    }

    public ISelectFieldsFromDepartmentSpecification Ver2()
    {
        SelectExpression = department => Domain.Entities.Department.InitFromDatabaseVer2(
            department.Id,
            department.Name,
            department.RemovedAt,
            department.RemovedBy);

        return this;
    }

    public ISelectFieldsFromDepartmentSpecification Ver3()
    {
        SelectExpression = department => Domain.Entities.Department.InitFromDatabaseVer3(
            department.Name);

        return this;
    }
}
