using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.UserJoiningStatus;
using FuDever.PostgresSql.Commons;
using Microsoft.EntityFrameworkCore;

namespace FuDever.PostgresSql.Specifications.Entities.UserJoiningStatus;

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
        WhereExpression = userJoiningStatus => EF.Functions
            .Collate(
                userJoiningStatus.Type,
                CommonConstant.DbCollation.CASE_INSENSITIVE)
            .Equals(userJoiningStatusType);
    }
}
