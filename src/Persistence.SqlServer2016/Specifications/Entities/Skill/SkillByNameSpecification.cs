using Domain.Specifications.Base;
using Domain.Specifications.Entities.Skill;
using Microsoft.EntityFrameworkCore;
using Persistence.SqlServer2016.Common;

namespace Persistence.SqlServer2016.Specifications.Entities.Skill;

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
                CustomConstant.DbCollation.SQL_LATIN1_GENERAL_CP1_CS_AS)
            .Equals(skillName);
    }
}
