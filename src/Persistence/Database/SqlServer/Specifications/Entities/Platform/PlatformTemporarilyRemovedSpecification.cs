using Domain.Specifications.Base;
using Domain.Specifications.Entities.Platform;
using Persistence.Commons;

namespace Persistence.Database.SqlServer.Specifications.Entities.Platform;

/// <summary>
///     Represent implementation of platform temporarily removed specification.
/// </summary>
internal sealed class PlatformTemporarilyRemovedSpecification :
    BaseSpecification<Domain.Entities.Platform>,
    IPlatformTemporarilyRemovedSpecification
{
    internal PlatformTemporarilyRemovedSpecification()
    {
        var minDateTimeInDatabase = CommonConstant.DbDefaultValue.MIN_DATE_TIME;

        WhereExpression = platform =>
            platform.RemovedBy != Application.Commons.CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID &&
            platform.RemovedAt != minDateTimeInDatabase;
    }
}
