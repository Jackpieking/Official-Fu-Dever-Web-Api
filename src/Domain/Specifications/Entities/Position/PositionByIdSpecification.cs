using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;
using System;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.Position;

public sealed class PositionByIdSpecification :
    GenericSpecification<PositionEntity>
{
    public PositionByIdSpecification(Guid positionId)
    {
        Criteria = position => position.Id == positionId;
    }
}
