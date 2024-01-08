using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;
using FuDeverWebApi.Helpers;
using System;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.Role;

public sealed class IsRoleNotSoftRemovedSpecification :
    GenericSpecification<AppRoleEntity>
{
    public IsRoleNotSoftRemovedSpecification()
    {
        var minDateTimeInDatabase = CustomConstants.App.MIN_DATETIME_SQL;

        Criteria = role =>
            role.DeletedBy == Guid.Empty &&
            role.DeletedAt == minDateTimeInDatabase;
    }
}