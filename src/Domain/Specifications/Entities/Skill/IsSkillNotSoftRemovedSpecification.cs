using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;
using FuDeverWebApi.Helpers;
using System;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.Skill;

public sealed class IsSkillNotSoftRemovedSpecification :
    GenericSpecification<SkillEntity>
{
    public IsSkillNotSoftRemovedSpecification()
    {
        var minDateTimeInDatabase = CustomConstants.App.MIN_DATETIME_SQL;

        Criteria = skill =>
            skill.DeletedBy == Guid.Empty &&
            skill.DeletedAt == minDateTimeInDatabase;
    }
}
