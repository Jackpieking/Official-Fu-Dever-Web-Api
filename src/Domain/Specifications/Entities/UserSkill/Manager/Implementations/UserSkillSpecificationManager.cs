using FuDeverWebApi.DataAccess.Specifications.Entites.UserSkill.Manager.Contracts;
using System;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.UserSkill.Manager.Implementations;

public sealed class UserSkillSpecificationManager : IUserSkillSpecificationManager
{
    private SelectFieldsFromUserSkillSpecification _selectFieldsFromUserSkillSpecification;

    public SelectFieldsFromUserSkillSpecification SelectFieldsFromUserSkillSpecification
    {
        get
        {
            _selectFieldsFromUserSkillSpecification ??= new();

            return _selectFieldsFromUserSkillSpecification;
        }
    }

    public UserSkillBySkillIdSpecification UserSkillBySkillIdSpecification(Guid skillId)
    {
        return new(skillId: skillId);
    }

    public UserSkillByUserIdSpecification UserSkillByUserIdSpecification(Guid userId)
    {
        return new(userId: userId);
    }
}
