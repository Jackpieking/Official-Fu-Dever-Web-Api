using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Hobby;
using FuDever.SqlServer.Commons;

namespace FuDever.SqlServer.Specifications.Entities.Hobby;

/// <summary>
///     Represent implementation of hobby temporarily removed specification.
/// </summary>
internal sealed class HobbyTemporarilyRemovedSpecification :
    BaseSpecification<Domain.Entities.Hobby>,
    IHobbyTemporarilyRemovedSpecification
{
    internal HobbyTemporarilyRemovedSpecification()
    {
        var minDateTimeInDatabase = CommonConstant.DbDefaultValue.MIN_DATE_TIME;

        WhereExpression = hobby =>
            hobby.RemovedBy != Application.Commons.CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID &&
            hobby.RemovedAt != minDateTimeInDatabase;
    }
}
