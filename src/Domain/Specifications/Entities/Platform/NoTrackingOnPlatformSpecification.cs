using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.Platform;

public sealed class NoTrackingOnPlatformSpecification :
    GenericSpecification<PlatformEntity>
{
    public NoTrackingOnPlatformSpecification()
    {
        IsAsNoTracking = true;
    }
}
