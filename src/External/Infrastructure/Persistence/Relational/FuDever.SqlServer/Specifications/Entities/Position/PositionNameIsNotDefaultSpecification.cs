using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Position;

namespace FuDever.SqlServer.Specifications.Entities.Position;

/// <summary>
///     Represent implementation of position
///     name is not default specification.
/// </summary>
internal sealed class PositionNameIsNotDefaultSpecification :
    BaseSpecification<Domain.Entities.Position>,
    IPositionNameIsNotDefaultSpecification
{
    public PositionNameIsNotDefaultSpecification()
    {
        WhereExpression = position => !position.Name.Equals(string.Empty);
    }
}
