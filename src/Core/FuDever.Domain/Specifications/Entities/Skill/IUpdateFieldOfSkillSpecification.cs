using FuDever.Domain.Specifications.Base;
using System;

namespace FuDever.Domain.Specifications.Entities.Skill;

/// <summary>
///     Represent update field of skill specification.
/// </summary>
public interface IUpdateFieldOfSkillSpecification : IBaseSpecification<Domain.Entities.Skill>
{
    IUpdateFieldOfSkillSpecification Ver1(
        DateTime skillRemovedAt,
        Guid skillRemovedBy);

    IUpdateFieldOfSkillSpecification Ver2(string skillName);
}
