using FuDever.Domain.Specifications.Base;
using System;

namespace FuDever.Domain.Specifications.Entities.UserHobby;

/// <summary>
///     Update field of user hobby specification.
/// </summary>
public interface IUpdateFieldOfUserHobbySpecification : IBaseSpecification<Domain.Entities.UserHobby>
{
    IUpdateFieldOfUserHobbySpecification Ver1(
        DateTime userUpdatedAt,
        Guid userUpdatedBy);
}
