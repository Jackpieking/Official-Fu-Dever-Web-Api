using System;
using Domain.Specifications.Base;
using Domain.Specifications.Entities.UserSkill;

namespace Persistence.SqlServer2016.Specifications.Entities.UserSkill;

/// <summary>
///     Represent implementation of user skill by user
///     id specification.
/// </summary>
internal sealed class UserSkillByUserIdSpecification :
    BaseSpecification<Domain.Entities.UserSkill>,
    IUserSkillByUserIdSpecification
{
    internal UserSkillByUserIdSpecification(Guid userId)
    {
        WhereExpression = userSkill => userSkill.UserId == userId;
    }
}
