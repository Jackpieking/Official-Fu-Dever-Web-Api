using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.UserSkill;
using System;

namespace FuDever.SqlServer.Specifications.Entities.UserSkill;

/// <summary>
///     Represent implementation of user skill by skill
///     id specification.
/// </summary>
internal sealed class UserSkillBySkillIdSpecification :
    BaseSpecification<Domain.Entities.UserSkill>,
    IUserSkillBySkillIdSpecification
{
    internal UserSkillBySkillIdSpecification(Guid skillId)
    {
        WhereExpression = userSkill => userSkill.SkillId == skillId;
    }
}
