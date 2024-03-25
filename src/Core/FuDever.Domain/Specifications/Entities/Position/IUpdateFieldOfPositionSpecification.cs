using FuDever.Domain.Specifications.Base;
using System;

namespace FuDever.Domain.Specifications.Entities.Position;

/// <summary>
///     Represent update field of position specification.
/// </summary>
public interface IUpdateFieldOfPositionSpecification : IBaseSpecification<Domain.Entities.Position>
{
    IUpdateFieldOfPositionSpecification Ver1(
        DateTime positionRemovedAt,
        Guid positionRemovedBy);

    IUpdateFieldOfPositionSpecification Ver2(string positionName);
}
