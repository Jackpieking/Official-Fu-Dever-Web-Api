using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Skill;

namespace FuDever.PostgresSql.Specifications.Entities.Skill;

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
