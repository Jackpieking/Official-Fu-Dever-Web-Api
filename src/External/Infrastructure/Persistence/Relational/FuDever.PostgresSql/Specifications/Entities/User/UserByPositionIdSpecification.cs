using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.User;
using System;

namespace FuDever.PostgresSql.Specifications.Entities.User;

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