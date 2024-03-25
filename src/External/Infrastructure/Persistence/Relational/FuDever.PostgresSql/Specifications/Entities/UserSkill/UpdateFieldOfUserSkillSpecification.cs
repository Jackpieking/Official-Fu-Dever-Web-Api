using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.UserSkill;
using System;

namespace FuDever.PostgresSql.Specifications.Entities.UserSkill;

/// <summary>
///     Represent implementation of update
///     field of user specification.
/// </summary>
internal sealed class UpdateFieldOfUserSkillSpecification :
    BaseSpecification<Domain.Entities.UserSkill>,
    IUpdateFieldOfUserSkillSpecification
{
    public IUpdateFieldOfUserSkillSpecification Ver1(
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
                    userSkill => userSkill.User.UpdatedAt,
                    userUpdatedAt)
                .SetProperty(
                    userSkill => userSkill.User.UpdatedBy,
                    userUpdatedBy));

        return this;
    }
}
