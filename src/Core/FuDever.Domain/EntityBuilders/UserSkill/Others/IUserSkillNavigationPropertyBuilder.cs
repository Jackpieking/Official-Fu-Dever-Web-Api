using FuDever.Domain.EntityBuilders.Others;

namespace FuDever.Domain.EntityBuilders.UserSkill.Others;

/// <summary>
///     Interface for user skill navigation property builder.
/// </summary>
public interface IUserSkillNavigationPropertyBuilder<TBuilder> :
    IBaseEntityHandler<Entities.UserSkill>
        where TBuilder : IBaseUserSkillBuilder
{
    /// <summary>
    ///     Sets skill.
    /// </summary>
    /// <param name="skill">
    ///     The skill.
    /// </param>
    /// <returns>
    ///     The builder.
    /// </returns>
    TBuilder WithSkill(Entities.Skill skill);
}
