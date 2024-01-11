using System;
using Domain.Specifications.Base;
using Domain.Specifications.Entities.Skill;

namespace Persistence.SqlServer2016.Specifications.Entities.Skill;

/// <summary>
///     Represent implementation of skill by
///     skill id specification.
/// </summary>
internal sealed class SkillByIdSpecification :
    BaseSpecification<Domain.Entities.Skill>,
    ISkillByIdSpecification
{
    internal SkillByIdSpecification(Guid skillId)
    {
        WhereExpression = skill => skill.Id == skillId;
    }
}
