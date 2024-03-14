using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Skill;
using FuDever.SqlServer.Commons;
using Microsoft.EntityFrameworkCore;

namespace FuDever.SqlServer.Specifications.Entities.Skill;

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
        if (!isCaseSensitive)
        {
            WhereExpression = skill => skill.Name.Equals(skillName);

            return;
        }

        WhereExpression = skill => EF.Functions
            .Collate(
                skill.Name,
                CommonConstant.DbCollation.SQL_LATIN1_GENERAL_CP1_CS_AS)
            .Equals(skillName);
    }
}
