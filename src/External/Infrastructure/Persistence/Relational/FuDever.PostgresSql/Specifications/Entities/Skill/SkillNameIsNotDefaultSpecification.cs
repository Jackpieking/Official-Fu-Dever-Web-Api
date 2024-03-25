using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Skill;

namespace FuDever.PostgresSql.Specifications.Entities.Skill;

/// <summary>
///     Represent implementation of skill name
///     is not default specification.
/// </summary>
internal sealed class SkillNameIsNotDefaultSpecification :
    BaseSpecification<Domain.Entities.Skill>,
    ISkillNameIsNotDefaultSpecification
{
    public SkillNameIsNotDefaultSpecification()
    {
        WhereExpression = skill => !skill.Name.Equals(string.Empty);
    }
}
