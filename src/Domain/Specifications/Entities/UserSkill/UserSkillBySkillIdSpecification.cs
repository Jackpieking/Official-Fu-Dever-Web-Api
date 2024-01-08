using System;
using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.UserSkill;

public sealed class UserSkillBySkillIdSpecification : GenericSpecification<UserSkillEntity>
{
    public UserSkillBySkillIdSpecification(Guid skillId)
    {
        Criteria = userSkill => userSkill.SkillId == skillId;
    }
}
