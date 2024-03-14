using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Position;
using FuDever.SqlServer.Commons;
using Microsoft.EntityFrameworkCore;

namespace FuDever.SqlServer.Specifications.Entities.Position;

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
        if (!isCaseSensitive)
        {
            WhereExpression = position => position.Name.Equals(positionName);

            return;
        }

        WhereExpression = position => EF.Functions
            .Collate(
                position.Name,
                CommonConstant.DbCollation.SQL_LATIN1_GENERAL_CP1_CS_AS)
            .Equals(positionName);
    }
}
