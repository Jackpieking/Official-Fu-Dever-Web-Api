using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.UserSkill;

namespace FuDever.SqlServer.Specifications.Entities.UserSkill;

/// <summary>
///     Represent implementation of update
///     field of user specification.
/// </summary>
internal sealed class UpdateFieldOfUserSkillSpecification :
    BaseSpecification<Domain.Entities.UserSkill>,
    IUpdateFieldOfUserSkillSpecification
{
}
