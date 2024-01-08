using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.UserJoiningStatus;

public sealed class NoTrackingOnUserJoiningStatusSpecification :
    GenericSpecification<UserJoiningStatusEntity>
{
    public NoTrackingOnUserJoiningStatusSpecification()
    {
        IsAsNoTracking = true;
    }
}
