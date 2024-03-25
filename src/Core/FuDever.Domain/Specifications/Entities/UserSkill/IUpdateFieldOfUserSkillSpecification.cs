using FuDever.Domain.Specifications.Base;
using System;

namespace FuDever.Domain.Specifications.Entities.UserSkill;

/// <summary>
///     Represent update field of user skill specification.
/// </summary>
public interface IUpdateFieldOfUserSkillSpecification : IBaseSpecification<Domain.Entities.UserSkill>
{
    IUpdateFieldOfUserSkillSpecification Ver1(
        DateTime userUpdatedAt,
        Guid userUpdatedBy);
}
