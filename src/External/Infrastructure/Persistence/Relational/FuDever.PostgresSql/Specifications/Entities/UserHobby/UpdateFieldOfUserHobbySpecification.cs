using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.UserHobby;
using System;

namespace FuDever.PostgresSql.Specifications.Entities.UserHobby;

/// <summary>
///     Represent implementation of update field of
///     user hobby specification.
/// </summary>
internal sealed class UpdateFieldOfUserHobbySpecification :
    BaseSpecification<Domain.Entities.UserHobby>,
    IUpdateFieldOfUserHobbySpecification
{
    public IUpdateFieldOfUserHobbySpecification Ver1(
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
                    userHobby => userHobby.User.UpdatedAt,
                    userUpdatedAt)
                .SetProperty(
                    userHobby => userHobby.User.UpdatedBy,
                    userUpdatedBy));

        return this;
    }
}
