using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.User;
using System;

namespace FuDever.PostgresSql.Specifications.Entities.User;

/// <summary>
///     Represent implementation of update field of user.
/// </summary>
internal sealed class UpdateFieldOfUserSpecification :
    BaseSpecification<Domain.Entities.User>,
    IUpdateFieldOfUserSpecification
{
    public IUpdateFieldOfUserSpecification Ver1(
        DateTime userUpdatedAt,
        Guid userUpdatedBy,
        Guid userDepartmentId)
    {
        // Validate user updator.
        if (userUpdatedBy == Guid.Empty)
        {
            return default;
        }

        // Validate user update time.
        if (userUpdatedAt == DateTime.MaxValue)
        {
            return default;
        }

        // Validate user department id.
        if (userDepartmentId == Guid.Empty)
        {
            return default;
        }

        UpdateExpression = setPropertyCall => setPropertyCall
            .SetProperty(
                user => user.UpdatedAt,
                userUpdatedAt)
            .SetProperty(
                user => user.UpdatedBy,
                userUpdatedBy)
            .SetProperty(
                user => user.DepartmentId,
                userDepartmentId);

        return this;
    }

    public IUpdateFieldOfUserSpecification Ver2(
        DateTime userUpdatedAt,
        Guid userUpdatedBy)
    {
        // Validate user updator.
        if (userUpdatedBy == Guid.Empty)
        {
            return default;
        }

        // Validate user update time.
        if (userUpdatedAt == DateTime.MaxValue)
        {
            return default;
        }

        UpdateExpression = setPropertyCall => setPropertyCall
            .SetProperty(
                user => user.UpdatedAt,
                userUpdatedAt)
            .SetProperty(
                user => user.UpdatedBy,
                userUpdatedBy);

        return this;
    }

    public IUpdateFieldOfUserSpecification Ver3(
        DateTime userUpdatedAt,
        Guid userUpdatedBy,
        Guid userMajorId)
    {
        // Validate user updator.
        if (userUpdatedBy == Guid.Empty)
        {
            return default;
        }

        // Validate user update time.
        if (userUpdatedAt == DateTime.MaxValue)
        {
            return default;
        }

        // Validate user major id.
        if (userMajorId == Guid.Empty)
        {
            return default;
        }

        UpdateExpression = setPropertyCall => setPropertyCall
            .SetProperty(
                user => user.UpdatedAt,
                userUpdatedAt)
            .SetProperty(
                user => user.UpdatedBy,
                userUpdatedBy)
            .SetProperty(
                user => user.MajorId,
                userMajorId);

        return this;
    }

    public IUpdateFieldOfUserSpecification Ver4(
        DateTime userUpdatedAt,
        Guid userUpdatedBy,
        Guid userPositionId)
    {
        // Validate user updator.
        if (userUpdatedBy == Guid.Empty)
        {
            return default;
        }

        // Validate user update time.
        if (userUpdatedAt == DateTime.MaxValue)
        {
            return default;
        }

        // Validate user position id.
        if (userPositionId == Guid.Empty)
        {
            return default;
        }

        UpdateExpression = setPropertyCall => setPropertyCall
            .SetProperty(
                user => user.UpdatedAt,
                userUpdatedAt)
            .SetProperty(
                user => user.UpdatedBy,
                userUpdatedBy)
            .SetProperty(
                user => user.PositionId,
                userPositionId);

        return this;
    }
}
