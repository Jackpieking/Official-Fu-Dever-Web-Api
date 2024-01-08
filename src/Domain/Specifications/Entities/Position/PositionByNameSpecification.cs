using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;
using FuDeverWebApi.Helpers;
using Microsoft.EntityFrameworkCore;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.Position;

public sealed class PositionByNameSpecification :
    GenericSpecification<PositionEntity>
{
    public PositionByNameSpecification(
        string positionName,
        bool isCaseSensitive = false)
    {
        if (!isCaseSensitive)
        {
            Criteria = position => position.Name.Equals(positionName);

            return;
        }

        Criteria = position => EF.Functions
            .Collate(
                position.Name,
                CustomConstants.SqlCollation.SQL_LATIN1_GENERAL_CP1_CS_AS)
            .Equals(positionName);
    }
}
