using System;
using Domain.Specifications.Base;
using Domain.Specifications.Entities.User;

namespace Persistence.SqlServer2016.Specifications.Entities.User;

/// <summary>
///     Represent implementation of user by position
///     id specification.
/// </summary>
internal sealed class UserByPositionIdSpecification :
    BaseSpecification<Domain.Entities.User>,
    IUserByPositionIdSpecification
{
    internal UserByPositionIdSpecification(Guid positionId)
    {
        WhereExpression = user => user.PositionId == positionId;
    }
}