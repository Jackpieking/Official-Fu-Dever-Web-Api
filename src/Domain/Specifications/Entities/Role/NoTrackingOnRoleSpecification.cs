using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.Role;

public sealed class NoTrackingOnRoleSpecification :
    GenericSpecification<AppRoleEntity>
{
    public NoTrackingOnRoleSpecification()
    {
        IsAsNoTracking = true;
    }
}
