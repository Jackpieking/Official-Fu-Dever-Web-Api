using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.UserSkill;

namespace FuDever.SqlServer.Specifications.Entities.UserSkill;

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
