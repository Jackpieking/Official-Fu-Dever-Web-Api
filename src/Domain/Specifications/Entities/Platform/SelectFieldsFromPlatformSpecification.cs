using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.Platform;

public sealed class SelectFieldsFromPlatformSpecification :
    GenericSpecification<PlatformEntity>
{
    public SelectFieldsFromPlatformSpecification Ver1()
    {
        SelectExpression = platform => new()
        {
            Id = platform.Id,
            Name = platform.Name
        };

        return this;
    }

    public SelectFieldsFromPlatformSpecification Ver2()
    {
        SelectExpression = platform => new()
        {
            Id = platform.Id,
            Name = platform.Name,
            DeletedAt = platform.DeletedAt,
            DeletedBy = platform.DeletedBy
        };

        return this;
    }
}
