using FuDever.Domain.EntityBuilders.Major;
using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Major;

namespace FuDever.SqlServer.Specifications.Entities.Major;

/// <summary>
///     Represent implementation of select fields from the "Majors"
///     table specification.
/// </summary>
internal sealed class SelectFieldsFromMajorSpecification :
    BaseSpecification<Domain.Entities.Major>,
    ISelectFieldsFromMajorSpecification
{
    public ISelectFieldsFromMajorSpecification Ver1()
    {
        MajorForDatabaseRetrievingBuilder builder = new();

        SelectExpression = major => builder
            .WithId(major.Id)
            .WithName(major.Name)
            .Complete();

        return this;
    }

    public ISelectFieldsFromMajorSpecification Ver2()
    {
        MajorForDatabaseRetrievingBuilder builder = new();

        SelectExpression = major => builder
            .WithId(major.Id)
            .WithName(major.Name)
            .WithRemovedAt(major.RemovedAt)
            .WithRemovedBy(major.RemovedBy)
            .Complete();

        return this;
    }

    public ISelectFieldsFromMajorSpecification Ver3()
    {
        MajorForDatabaseRetrievingBuilder builder = new();

        SelectExpression = major => builder
            .WithName(major.Name)
            .Complete();

        return this;
    }
}
