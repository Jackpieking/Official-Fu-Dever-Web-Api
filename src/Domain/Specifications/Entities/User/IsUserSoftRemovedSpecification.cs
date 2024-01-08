using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;
using FuDeverWebApi.Helpers;
using System;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.User;

public sealed class IsUserSoftRemovedSpecification :
    GenericSpecification<AppUserEntity>
{
    public IsUserSoftRemovedSpecification()
    {
        var minDateTimeInDatabase = CustomConstants.App.MIN_DATETIME_SQL;

        Criteria = user =>
            user.DeletedBy != Guid.Empty &&
            user.DeletedAt != minDateTimeInDatabase;
    }
}
