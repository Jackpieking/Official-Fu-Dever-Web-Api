using FuDever.Domain.EntityBuilders.Others;
using System;

namespace FuDever.Domain.EntityBuilders.UserSkill.Others;

/// <summary>
///     Interface for user skill builder.
/// </summary>
public interface IUserSkillBuilder<TBuilder> :
    IBaseEntityHandler<Entities.UserSkill>
        where TBuilder : IBaseUserSkillBuilder
{
    /// <summary>
    ///     Set the user id.
    /// </summary>
    /// <param name="userId">
    ///     The user id.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithUserId(Guid userId);

    /// <summary>
    ///     Set the skill id.
    /// </summary>
    /// <param name="skillId">
    ///     The skill id.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithSkillId(Guid skillId);
}
