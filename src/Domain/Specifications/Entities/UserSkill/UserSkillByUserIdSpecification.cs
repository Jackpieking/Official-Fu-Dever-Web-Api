using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;
using System;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.UserSkill;

public sealed class UserSkillByUserIdSpecification : GenericSpecification<UserSkillEntity>
{
    public UserSkillByUserIdSpecification(Guid userId)
    {
        Criteria = userSkill => userSkill.UserId == userId;
    }
}
