using FuDever.Domain.Specifications.Base;
using System;

namespace FuDever.Domain.Specifications.Entities.Major;

/// <summary>
///     Represent update field of major specification.
/// </summary>
public interface IUpdateFieldOfMajorSpecification : IBaseSpecification<Domain.Entities.Major>
{
    IUpdateFieldOfMajorSpecification Ver1(
        DateTime majorRemovedAt,
        Guid majorRemovedBy);

    IUpdateFieldOfMajorSpecification Ver2(string majorName);
}
