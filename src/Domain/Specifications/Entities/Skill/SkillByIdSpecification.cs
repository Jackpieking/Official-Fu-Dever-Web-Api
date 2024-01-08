using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;
using System;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.Skill;

public sealed class SkillByIdSpecification :
    GenericSpecification<SkillEntity>
{
    public SkillByIdSpecification(Guid skillId)
    {
        Criteria = skill => skill.Id == skillId;
    }
}
