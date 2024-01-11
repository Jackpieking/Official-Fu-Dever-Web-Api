using Domain.Specifications.Base;
using Domain.Specifications.Entities.Position;
using Microsoft.EntityFrameworkCore;
using Persistence.SqlServer2016.Common;

namespace Persistence.SqlServer2016.Specifications.Entities.Position;

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
                CustomConstant.DbCollation.SQL_LATIN1_GENERAL_CP1_CS_AS)
            .Equals(positionName);
    }
}
