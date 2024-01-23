using Domain.Specifications.Entities.UserSkill;
using Domain.Specifications.Entities.UserSkill.Manager;
using System;

namespace Persistence.Database.SqlServer.Specifications.Entities.UserSkill.Manager;

/// <summary>
///     Represent implementation of user skill specification manager.
/// </summary>
internal sealed class UserSkillSpecificationManager : IUserSkillSpecificationManager
{
    // Backing fields.
    private ISelectFieldsFromUserSkillSpecification _selectFieldsFromUserSkillSpecification;

    public ISelectFieldsFromUserSkillSpecification SelectFieldsFromUserSkillSpecification
    {
        get
        {
            _selectFieldsFromUserSkillSpecification ??= new SelectFieldsFromUserSkillSpecification();

            return _selectFieldsFromUserSkillSpecification;
        }
    }

    public IUserSkillBySkillIdSpecification UserSkillBySkillIdSpecification(Guid skillId)
    {
        return new UserSkillBySkillIdSpecification(skillId: skillId);
    }

    public IUserSkillByUserIdSpecification UserSkillByUserIdSpecification(Guid userId)
    {
        return new UserSkillByUserIdSpecification(userId: userId);
    }
}
