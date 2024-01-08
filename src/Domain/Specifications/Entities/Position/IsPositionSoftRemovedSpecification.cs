using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;
using FuDeverWebApi.Helpers;
using System;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.Position;

public sealed class IsPositionSoftRemovedSpecification :
    GenericSpecification<PositionEntity>
{
    public IsPositionSoftRemovedSpecification()
    {
        var minDateTimeInDatabase = CustomConstants.App.MIN_DATETIME_SQL;

        Criteria = position =>
            position.DeletedBy != Guid.Empty &&
            position.DeletedAt != minDateTimeInDatabase;
    }
}
