using Domain.Specifications.Base;
using Domain.Specifications.Entities.UserSkill;

namespace Persistence.Database.SqlServer.Specifications.Entities.UserSkill;

/// <summary>
///     Represent implementation of user
///     skill as no tracking specification
/// </summary>
internal sealed class UserSkillAsNoTrackingSpecification :
    BaseSpecification<Domain.Entities.UserSkill>,
    IUserSkillAsNoTrackingSpecification
{
    public UserSkillAsNoTrackingSpecification()
    {
        IsAsNoTracking = true;
    }
}
