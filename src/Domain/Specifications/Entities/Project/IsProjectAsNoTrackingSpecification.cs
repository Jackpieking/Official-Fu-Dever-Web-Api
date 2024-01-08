using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.Project;

public sealed class NoTrackingOnProjectSpecification :
    GenericSpecification<ProjectEntity>
{
    public NoTrackingOnProjectSpecification()
    {
        IsAsNoTracking = true;
    }
}
