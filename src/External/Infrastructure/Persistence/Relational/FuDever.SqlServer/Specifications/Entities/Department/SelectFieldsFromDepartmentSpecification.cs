using FuDever.Domain.EntityBuilders.Department;
using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Department;

namespace FuDever.SqlServer.Specifications.Entities.Department;

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
        DepartmentForDatabaseRetrievingBuilder builder = new();

        SelectExpression = department => builder
            .WithId(department.Id)
            .WithName(department.Name)
            .Complete();

        return this;
    }

    public ISelectFieldsFromDepartmentSpecification Ver2()
    {
        DepartmentForDatabaseRetrievingBuilder builder = new();

        SelectExpression = department => builder
            .WithId(department.Id)
            .WithName(department.Name)
            .WithRemovedAt(department.RemovedAt)
            .WithRemovedBy(department.RemovedBy)
            .Complete();

        return this;
    }

    public ISelectFieldsFromDepartmentSpecification Ver3()
    {
        DepartmentForDatabaseRetrievingBuilder builder = new();

        SelectExpression = department => builder
            .WithName(department.Name)
            .Complete();

        return this;
    }
}
