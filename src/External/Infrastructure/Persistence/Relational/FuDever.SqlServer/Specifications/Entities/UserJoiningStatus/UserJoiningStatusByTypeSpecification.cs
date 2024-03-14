using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.UserJoiningStatus;

namespace FuDever.SqlServer.Specifications.Entities.UserJoiningStatus;

/// <summary>
///     Represent implementation of user joining status by
///     user joining status type specification.
/// </summary>
internal sealed class UserJoiningStatusByTypeSpecification :
    BaseSpecification<Domain.Entities.UserJoiningStatus>,
    IUserJoiningStatusByTypeSpecification
{
    internal UserJoiningStatusByTypeSpecification(string userJoiningStatusType)
    {
        WhereExpression = userJoiningStatus => userJoiningStatus.Type.Equals(userJoiningStatusType);
    }
}
