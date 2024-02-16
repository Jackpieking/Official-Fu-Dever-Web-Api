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
    private IUserSkillAsNoTrackingSpecification _userSkillAsNoTrackingSpecification;

    public ISelectFieldsFromUserSkillSpecification SelectFieldsFromUserSkillSpecification
    {
        get
        {
            _selectFieldsFromUserSkillSpecification ??= new SelectFieldsFromUserSkillSpecification();

            return _selectFieldsFromUserSkillSpecification;
        }
    }

    public IUserSkillAsNoTrackingSpecification UserSkillAsNoTrackingSpecification
    {
        get
        {
            _userSkillAsNoTrackingSpecification = new UserSkillAsNoTrackingSpecification();

            return _userSkillAsNoTrackingSpecification;
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
