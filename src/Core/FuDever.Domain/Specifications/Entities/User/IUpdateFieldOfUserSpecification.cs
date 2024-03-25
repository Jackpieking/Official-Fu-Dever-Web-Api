using FuDever.Domain.Specifications.Base;
using System;

namespace FuDever.Domain.Specifications.Entities.User;

/// <summary>
///     Represent update field of user specification.
/// </summary>
public interface IUpdateFieldOfUserSpecification : IBaseSpecification<Domain.Entities.User>
{
    IUpdateFieldOfUserSpecification Ver1(
        DateTime userUpdatedAt,
        Guid userUpdatedBy,
        Guid userDepartmentId);

    IUpdateFieldOfUserSpecification Ver2(
        DateTime userUpdatedAt,
        Guid userUpdatedBy);

    IUpdateFieldOfUserSpecification Ver3(
        DateTime userUpdatedAt,
        Guid userUpdatedBy,
        Guid userMajorId);

    IUpdateFieldOfUserSpecification Ver4(
        DateTime userUpdatedAt,
        Guid userUpdatedBy,
        Guid userPositionId);
}
