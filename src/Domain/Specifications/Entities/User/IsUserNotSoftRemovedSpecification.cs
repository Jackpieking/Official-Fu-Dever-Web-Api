using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;
using FuDeverWebApi.Helpers;
using System;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.User;

public sealed class IsUserNotSoftRemovedSpecification :
    GenericSpecification<AppUserEntity>
{
    public IsUserNotSoftRemovedSpecification()
    {
        var minDateTimeInDatabase = CustomConstants.App.MIN_DATETIME_SQL;

        Criteria = user =>
            user.DeletedBy == Guid.Empty &&
            user.DeletedAt == minDateTimeInDatabase;
    }
}
