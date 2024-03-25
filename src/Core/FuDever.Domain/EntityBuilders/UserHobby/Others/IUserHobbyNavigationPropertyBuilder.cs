using FuDever.Domain.EntityBuilders.Others;

namespace FuDever.Domain.EntityBuilders.UserHobby.Others;

/// <summary>
///     Interface for user hobby navigation property builder.
/// </summary>
public interface IUserHobbyNavigationPropertyBuilder<TBuilder> :
    IBaseEntityHandler<Entities.UserHobby>
        where TBuilder : IBaseUserHobbyBuilder
{
    /// <summary>
    ///     Sets hobby.
    /// </summary>
    /// <param name="hobby">
    ///     The hobby.
    /// </param>
    /// <returns>
    ///     The builder.
    /// </returns>
    TBuilder WithHobby(Entities.Hobby hobby);
}
