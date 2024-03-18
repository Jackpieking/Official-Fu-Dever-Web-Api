using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Position;
using FuDever.PostgresSql.Commons;
using Microsoft.EntityFrameworkCore;

namespace FuDever.PostgresSql.Specifications.Entities.Position;

/// <summary>
///     Represent implementation of position by position name specification.
/// </summary>
internal sealed class PositionByNameSpecification :
    BaseSpecification<Domain.Entities.Position>,
    IPositionByNameSpecification
{
    internal PositionByNameSpecification(
        string positionName,
        bool isCaseSensitive)
    {
        if (isCaseSensitive)
        {
            WhereExpression = position => position.Name.Equals(positionName);

            return;
        }

        WhereExpression = position => EF.Functions
            .Collate(
                position.Name,
                CommonConstant.DbCollation.CASE_INSENSITIVE)
            .Equals(positionName);
    }
}
