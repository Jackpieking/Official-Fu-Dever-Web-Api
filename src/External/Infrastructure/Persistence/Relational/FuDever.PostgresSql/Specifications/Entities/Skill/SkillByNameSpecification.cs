using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Skill;
using FuDever.PostgresSql.Commons;
using Microsoft.EntityFrameworkCore;

namespace FuDever.PostgresSql.Specifications.Entities.Skill;

/// <summary>
///     Represent implementation of skill by
///     skill name specification.
/// </summary>
internal sealed class SkillByNameSpecification :
    BaseSpecification<Domain.Entities.Skill>,
    ISkillByNameSpecification
{
    internal SkillByNameSpecification(
        string skillName,
        bool isCaseSensitive)
    {
        if (isCaseSensitive)
        {
            WhereExpression = skill => skill.Name.Equals(skillName);

            return;
        }

        WhereExpression = skill => EF.Functions
            .Collate(
                skill.Name,
                CommonConstant.DbCollation.CASE_INSENSITIVE)
            .Equals(skillName);
    }
}
