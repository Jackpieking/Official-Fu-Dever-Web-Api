using FuDever.Domain.EntityBuilders.Others;
using System;

namespace FuDever.Domain.EntityBuilders.Skill.Others;

/// <summary>
///     Interface for skill builder.
/// </summary>
public interface ISkillBuilder<TBuilder> :
    IBaseEntityHandler<Entities.Skill>
        where TBuilder : IBaseSkillBuilder
{
    /// <summary>
    ///     Set the id of skill.
    /// </summary>
    /// <param name="skillId">
    ///     Id of skill.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithId(Guid skillId);

    /// <summary>
    ///     Set the name of skill.
    /// </summary>
    /// <param name="skillName">
    ///     Name of skill.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithName(string skillName);

    /// <summary>
    ///     Set the remove time of skill.
    /// </summary>
    /// <param name="skillRemovedAt">
    ///     Remove time of skill.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithRemovedAt(DateTime skillRemovedAt);

    /// <summary>
    ///     Set the remover of skill.
    /// </summary>
    /// <param name="skillRemovedBy">
    ///     Remover of skill.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithRemovedBy(Guid skillRemovedBy);
}
