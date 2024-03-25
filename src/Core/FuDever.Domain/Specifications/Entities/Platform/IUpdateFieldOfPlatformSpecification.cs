using FuDever.Domain.Specifications.Base;
using System;

namespace FuDever.Domain.Specifications.Entities.Platform;

/// <summary>
///     Represent update field of platform specification.
/// </summary>
public interface IUpdateFieldOfPlatformSpecification : IBaseSpecification<Domain.Entities.Platform>
{
    IUpdateFieldOfPlatformSpecification Ver1(
        DateTime platformRemovedAt,
        Guid platformRemovedBy);

    IUpdateFieldOfPlatformSpecification Ver2(string platformName);
}
