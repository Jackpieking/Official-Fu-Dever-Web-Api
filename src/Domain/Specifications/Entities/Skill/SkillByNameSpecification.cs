using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;
using FuDeverWebApi.Helpers;
using Microsoft.EntityFrameworkCore;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.Skill;

public sealed class SkillByNameSpecification :
    GenericSpecification<SkillEntity>
{
    public SkillByNameSpecification(
        string skillName,
        bool isCaseSensitive = false)
    {
        if (!isCaseSensitive)
        {
            Criteria = skill => skill.Name.Equals(skillName);

            return;
        }

        Criteria = skill => EF.Functions
            .Collate(
                skill.Name,
                CustomConstants.SqlCollation.SQL_LATIN1_GENERAL_CP1_CS_AS)
            .Equals(skillName);
    }
}
