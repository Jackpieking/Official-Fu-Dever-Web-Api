using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.User;

public sealed class NoTrackingOnUserSpecification :
    GenericSpecification<AppUserEntity>
{
    public NoTrackingOnUserSpecification()
    {
        IsAsNoTracking = true;
    }
}
