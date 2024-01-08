using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;
using FuDeverWebApi.Helpers;
using System;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.Platform;

public sealed class IsPlatformNotSoftRemovedSpecification :
    GenericSpecification<PlatformEntity>
{
    public IsPlatformNotSoftRemovedSpecification()
    {
        var minDateTimeInDatabase = CustomConstants.App.MIN_DATETIME_SQL;

        Criteria = platform =>
            platform.DeletedBy == Guid.Empty &&
            platform.DeletedAt == minDateTimeInDatabase;
    }
}
