using FuDever.Domain.Specifications.Base;
using System;

namespace FuDever.Domain.Specifications.Entities.Hobby;

/// <summary>
///     Represent update field of hobby specification.
/// </summary>
public interface IUpdateFieldOfHobbySpecification : IBaseSpecification<Domain.Entities.Hobby>
{
    IUpdateFieldOfHobbySpecification Ver1(
        DateTime hobbyRemovedAt,
        Guid hobbyRemovedBy);

    IUpdateFieldOfHobbySpecification Ver2(string hobbyName);
}
