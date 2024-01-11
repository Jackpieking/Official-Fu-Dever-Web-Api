using Domain.Specifications.Base;
using Domain.Specifications.Entities.Skill;

namespace Persistence.SqlServer2016.Specifications.Entities.Skill;

/// <summary>
///     Represent implementation of skill as no
///     tracking specification.
/// </summary>
internal sealed class SkillAsNoTrackingSpecification :
    BaseSpecification<Domain.Entities.Skill>,
    ISkillAsNoTrackingSpecification
{
    internal SkillAsNoTrackingSpecification()
    {
        IsAsNoTracking = true;
    }
}
