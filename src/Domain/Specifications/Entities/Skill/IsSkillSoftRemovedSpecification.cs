using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;
using FuDeverWebApi.Helpers;
using System;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.Skill;

public sealed class IsSkillSoftRemovedSpecification :
    GenericSpecification<SkillEntity>
{
    public IsSkillSoftRemovedSpecification()
    {
        var minDateTimeInDatabase = CustomConstants.App.MIN_DATETIME_SQL;

        Criteria = skill =>
            skill.DeletedBy != Guid.Empty &&
            skill.DeletedAt != minDateTimeInDatabase;
    }
}
