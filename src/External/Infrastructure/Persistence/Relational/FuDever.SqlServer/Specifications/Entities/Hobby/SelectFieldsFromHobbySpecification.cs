using FuDever.Domain.EntityBuilders.Hobby;
using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Hobby;

namespace FuDever.SqlServer.Specifications.Entities.Hobby;

/// <summary>
///     Represent implementation of select fields from the "Hobbies"
///     table specification.
/// </summary>
internal sealed class SelectFieldsFromHobbySpecification :
    BaseSpecification<Domain.Entities.Hobby>,
    ISelectFieldsFromHobbySpecification
{
    public ISelectFieldsFromHobbySpecification Ver1()
    {
        HobbyForDatabaseRetrievingBuilder builder = new();

        SelectExpression = hobby => builder
            .WithId(hobby.Id)
            .WithName(hobby.Name)
            .Complete();

        return this;
    }

    public ISelectFieldsFromHobbySpecification Ver2()
    {
        HobbyForDatabaseRetrievingBuilder builder = new();

        SelectExpression = hobby => builder
            .WithId(hobby.Id)
            .WithName(hobby.Name)
            .WithRemovedAt(hobby.RemovedAt)
            .WithRemovedBy(hobby.RemovedBy)
            .Complete();

        return this;
    }

    public ISelectFieldsFromHobbySpecification Ver3()
    {
        HobbyForDatabaseRetrievingBuilder builder = new();

        SelectExpression = hobby => builder
            .WithId(hobby.Id)
            .Complete();

        return this;
    }

    public ISelectFieldsFromHobbySpecification Ver4()
    {
        HobbyForDatabaseRetrievingBuilder builder = new();

        SelectExpression = hobby => builder
            .WithName(hobby.Name)
            .Complete();

        return this;
    }
}
