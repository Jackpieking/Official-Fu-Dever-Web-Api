using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.UserRole;
using System;

namespace FuDever.SqlServer.Specifications.Entities.UserRole;

/// <summary>
///     Represent implementation of update
///     field of user role specification.
/// </summary>
internal sealed class UpdateFieldOfUserRoleSpecification :
    BaseSpecification<Domain.Entities.UserRole>,
    IUpdateFieldOfUserRoleSpecification
{
    public IUpdateFieldOfUserRoleSpecification Ver1(
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
                    userRole => userRole.User.UpdatedAt,
                    userUpdatedAt)
                .SetProperty(
                    userRole => userRole.User.UpdatedBy,
                    userUpdatedBy));

        return this;
    }
}
