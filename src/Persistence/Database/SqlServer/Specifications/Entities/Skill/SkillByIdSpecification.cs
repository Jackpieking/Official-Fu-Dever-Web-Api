using Domain.Specifications.Base;
using Domain.Specifications.Entities.Skill;
using System;

namespace Persistence.Database.SqlServer.Specifications.Entities.Skill;

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