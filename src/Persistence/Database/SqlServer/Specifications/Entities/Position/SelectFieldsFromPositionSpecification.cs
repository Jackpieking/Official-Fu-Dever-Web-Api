using Domain.Specifications.Base;
using Domain.Specifications.Entities.Position;

namespace Persistence.Database.SqlServer.Specifications.Entities.Position;

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
        SelectExpression = position => Domain.Entities.Position.InitVer3(
            position.Id,
            position.Name);

        return this;
    }

    public ISelectFieldsFromPositionSpecification Ver2()
    {
        SelectExpression = position => Domain.Entities.Position.InitVer1(
            position.Id,
            position.Name,
            position.RemovedAt,
            position.RemovedBy);

        return this;
    }

    public ISelectFieldsFromPositionSpecification Ver3()
    {
        SelectExpression = position => Domain.Entities.Position.InitVer2(position.Name);

        return this;
    }
}
