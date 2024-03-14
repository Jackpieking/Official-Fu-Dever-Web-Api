using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Skill;
using System;

namespace FuDever.SqlServer.Specifications.Entities.Skill;

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
