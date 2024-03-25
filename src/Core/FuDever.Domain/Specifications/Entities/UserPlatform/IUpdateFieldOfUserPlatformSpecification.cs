using FuDever.Domain.Specifications.Base;
using System;

namespace FuDever.Domain.Specifications.Entities.UserPlatform;

/// <summary>
///     Represent update field of user platform specification.
/// </summary>
public interface IUpdateFieldOfUserPlatformSpecification : IBaseSpecification<Domain.Entities.UserPlatform>
{
    IUpdateFieldOfUserPlatformSpecification Ver1(
        DateTime userUpdatedAt,
        Guid userUpdatedBy);
}
