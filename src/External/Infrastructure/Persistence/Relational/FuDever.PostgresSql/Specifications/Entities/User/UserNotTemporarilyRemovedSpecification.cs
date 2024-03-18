using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.User;
using FuDever.PostgresSql.Commons;

namespace FuDever.PostgresSql.Specifications.Entities.User;

/// <summary>
///     Represent implementation of user not temporarily
///     removed specification.
/// </summary>
internal sealed class UserNotTemporarilyRemovedSpecification :
    BaseSpecification<Domain.Entities.User>,
    IUserNotTemporarilyRemovedSpecification
{
    internal UserNotTemporarilyRemovedSpecification()
    {
        var minDateTimeInDatabase = CommonConstant.DbDefaultValue.MIN_DATE_TIME;

        WhereExpression = user =>
            user.RemovedBy == Application.Commons.CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID &&
            user.RemovedAt == minDateTimeInDatabase;
    }
}
