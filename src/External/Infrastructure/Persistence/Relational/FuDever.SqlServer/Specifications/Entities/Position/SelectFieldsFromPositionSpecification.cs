using FuDever.Domain.EntityBuilders.Position;
using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Position;

namespace FuDever.SqlServer.Specifications.Entities.Position;

/// <summary>
///     Represent implementation of select fields from the "Positions"
///     table specification.
/// </summary>
internal sealed class SelectFieldsFromPositionSpecification :
    BaseSpecification<Domain.Entities.Position>,
    ISelectFieldsFromPositionSpecification
{
    public ISelectFieldsFromPositionSpecification Ver1()
    {
        PositionForDatabaseRetrievingBuilder builder = new();

        SelectExpression = position => builder
            .WithId(position.Id)
            .WithName(position.Name)
            .Complete();

        return this;
    }

    public ISelectFieldsFromPositionSpecification Ver2()
    {
        PositionForDatabaseRetrievingBuilder builder = new();

        SelectExpression = position => builder
            .WithId(position.Id)
            .WithName(position.Name)
            .WithRemovedAt(position.RemovedAt)
            .WithRemovedBy(position.RemovedBy)
            .Complete();

        return this;
    }

    public ISelectFieldsFromPositionSpecification Ver3()
    {
        PositionForDatabaseRetrievingBuilder builder = new();

        SelectExpression = position => builder
            .WithName(position.Name)
            .Complete();

        return this;
    }
}
