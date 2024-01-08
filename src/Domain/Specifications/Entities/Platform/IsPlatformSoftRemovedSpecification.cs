using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;
using FuDeverWebApi.Helpers;
using System;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.Platform;

public sealed class IsPlatformSoftRemovedSpecification :
    GenericSpecification<PlatformEntity>
{
    public IsPlatformSoftRemovedSpecification()
    {
        var minDateTimeInDatabase = CustomConstants.App.MIN_DATETIME_SQL;

        Criteria = platform =>
            platform.DeletedBy != Guid.Empty &&
            platform.DeletedAt != minDateTimeInDatabase;
    }
}
