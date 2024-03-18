using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.UserHobby;
using System;

namespace FuDever.PostgresSql.Specifications.Entities.UserHobby;

/// <summary>
///     Represent implementation of user hobby by hobby
///     id specification.
/// </summary>
internal sealed class UserHobbyByHobbyIdSpecification :
    BaseSpecification<Domain.Entities.UserHobby>,
    IUserHobbyByHobbyIdSpecification
{
    public UserHobbyByHobbyIdSpecification(Guid hobbyId)
    {
        WhereExpression = userHobby => userHobby.HobbyId == hobbyId;
    }
}
