using Domain.Specifications.Base;
using Domain.Specifications.Entities.Position;

namespace Persistence.SqlServer2016.Specifications.Entities.Position;

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
        SelectExpression = position => new()
        {
            Id = position.Id,
            Name = position.Name
        };

        return this;
    }

    public ISelectFieldsFromPositionSpecification Ver2()
    {
        SelectExpression = position => new()
        {
            Id = position.Id,
            Name = position.Name,
            RemovedAt = position.RemovedAt,
            RemovedBy = position.RemovedBy
        };

        return this;
    }
}
