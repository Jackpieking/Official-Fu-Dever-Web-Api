using System;

namespace Domain.Specifications.Entities.Skill.Manager;

/// <summary>
///     Represent skill specification manager.
/// </summary>
public interface ISkillSpecificationManager
{
    /// <summary>
    ///     Skill not temporarily removed specification.
    /// </summary>
    ISkillNotTemporarilyRemovedSpecification SkillNotTemporarilyRemovedSpecification { get; }

    /// <summary>
    ///     Skill temporarily removed specification.
    /// </summary>
    ISkillTemporarilyRemovedSpecification SkillTemporarilyRemovedSpecification { get; }

    /// <summary>
    ///     Skill as no tracking specification.
    /// </summary>
    ISkillAsNoTrackingSpecification SkillAsNoTrackingSpecification { get; }

    /// <summary>
    ///     Select field from "Skills" table specification.
    /// </summary>
    ISelectFieldsFromSkillSpecification SelectFieldsFromSkillSpecification { get; }

    /// <summary>
    ///     Skill by skill id specification.
    /// </summary>
    /// <param name="skillId">
    ///     Skill id for finding skill.
    /// </param>
    /// <returns>
    ///     Specification.
    /// </returns>
    ISkillByIdSpecification SkillByIdSpecification(Guid skillId);

    /// <summary>
    ///     Skill by skill name specification.
    /// </summary>
    /// <param name="skillName">
    ///     Skill name for finding skill.
    /// </param>
    /// <param name="isCaseSensitive">
    ///     Does skill name need searching in a sensitive way.
    /// </param>
    /// <returns>
    ///     Specification.
    /// </returns>
    ISkillByNameSpecification SkillByNameSpecification(
        string skillName,
        bool isCaseSensitive);
}
