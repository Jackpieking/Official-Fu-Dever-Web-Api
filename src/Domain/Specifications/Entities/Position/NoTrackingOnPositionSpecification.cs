using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.Position;

public sealed class NoTrackingOnPositionSpecification :
    GenericSpecification<PositionEntity>
{
    public NoTrackingOnPositionSpecification()
    {
        IsAsNoTracking = true;
    }
}