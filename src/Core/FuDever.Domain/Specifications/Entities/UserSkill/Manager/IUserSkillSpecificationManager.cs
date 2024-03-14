using System;

namespace FuDever.Domain.Specifications.Entities.UserSkill.Manager;

/// <summary>
///     Represent user skill specification manager.
/// </summary>
public interface IUserSkillSpecificationManager
{
    /// <summary>
    ///     User skill by user id specification.
    /// </summary>
    /// <param name="userId">
    ///     User id for finding user skill.
    /// </param>
    /// <returns>
    ///     Specification.
    /// </returns>
    IUserSkillByUserIdSpecification UserSkillByUserIdSpecification(Guid userId);

    /// <summary>
    ///     Select field from "UserSkills" table specification.
    /// </summary>
    ISelectFieldsFromUserSkillSpecification SelectFieldsFromUserSkillSpecification { get; }

    /// <summary>
    ///     User skill as no tracking specification.
    /// </summary>
    IUserSkillAsNoTrackingSpecification UserSkillAsNoTrackingSpecification { get; }

    /// <summary>
    ///     User skill by skill id specification.
    /// </summary>
    /// <param name="skillId">
    ///     Skill id for finding user skill.
    /// </param>
    /// <returns>
    ///     Specification.
    /// </returns>
    IUserSkillBySkillIdSpecification UserSkillBySkillIdSpecification(Guid skillId);
}
