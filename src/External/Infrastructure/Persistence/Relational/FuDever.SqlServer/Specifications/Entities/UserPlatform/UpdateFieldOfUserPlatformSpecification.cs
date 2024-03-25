using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.UserPlatform;
using System;

namespace FuDever.SqlServer.Specifications.Entities.UserPlatform;

/// <summary>
///     Represent implementation of update field of
///     user platform specification.
/// </summary>
internal sealed class UpdateFieldOfUserPlatformSpecification :
    BaseSpecification<Domain.Entities.UserPlatform>,
    IUpdateFieldOfUserPlatformSpecification
{
    public IUpdateFieldOfUserPlatformSpecification Ver1(
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

        UpdateExpression = setPropertyCall => setPropertyCall;

        UpdateExpression = AppendSetProperty(
            left: UpdateExpression,
            right: setPropertyCall => setPropertyCall
                .SetProperty(
                    userPlatform => userPlatform.User.UpdatedAt,
                    userUpdatedAt)
                .SetProperty(
                    userPlatform => userPlatform.User.UpdatedBy,
                    userUpdatedBy));

        return this;
    }
}
