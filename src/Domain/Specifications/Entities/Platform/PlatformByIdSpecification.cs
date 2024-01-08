using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;
using System;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.Platform;

public sealed class PlatformByIdSpecification :
    GenericSpecification<PlatformEntity>
{
    public PlatformByIdSpecification(Guid platformId)
    {
        Criteria = platform => platform.Id == platformId;
    }
}
