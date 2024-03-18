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
        SelectExpression = position => Domain.Entities.Position.InitFromDatabaseVer2(
            position.Id,
            position.Name);

        return this;
    }

    public ISelectFieldsFromPositionSpecification Ver2()
    {
        SelectExpression = position => Domain.Entities.Position.InitFromDatabaseVer3(
            position.Id,
            position.Name,
            position.RemovedAt,
            position.RemovedBy);

        return this;
    }

    public ISelectFieldsFromPositionSpecification Ver3()
    {
        SelectExpression = position => Domain.Entities.Position.InitFromDatabaseVer1(position.Name);

        return this;
    }
}
