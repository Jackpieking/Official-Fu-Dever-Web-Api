using FuDever.Domain.Specifications.Base;

namespace FuDever.Domain.Specifications.Entities.Department;

/// <summary>
///     Represent select fields from the "Departments" table specification.
/// </summary>
public interface ISelectFieldsFromDepartmentSpecification : IBaseSpecification<Domain.Entities.Department>
{
    ISelectFieldsFromDepartmentSpecification Ver1();

    ISelectFieldsFromDepartmentSpecification Ver2();

    ISelectFieldsFromDepartmentSpecification Ver3();
}
