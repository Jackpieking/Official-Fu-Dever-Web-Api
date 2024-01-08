using System;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.UserSkill.Manager.Contracts;

public interface IUserSkillSpecificationManager
{
    UserSkillByUserIdSpecification UserSkillByUserIdSpecification(Guid userId);

    SelectFieldsFromUserSkillSpecification SelectFieldsFromUserSkillSpecification { get; }

    UserSkillBySkillIdSpecification UserSkillBySkillIdSpecification(Guid skillId);
}
