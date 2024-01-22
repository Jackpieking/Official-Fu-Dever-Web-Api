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
        SelectExpression = position => Domain.Entities.Position.Init(
            position.Id,
            position.Name);

        return this;
    }

    public ISelectFieldsFromPositionSpecification Ver2()
    {
        SelectExpression = position => Domain.Entities.Position.Init(
            position.Id,
            position.Name,
            position.RemovedAt,
            position.RemovedBy);

        return this;
    }
}
