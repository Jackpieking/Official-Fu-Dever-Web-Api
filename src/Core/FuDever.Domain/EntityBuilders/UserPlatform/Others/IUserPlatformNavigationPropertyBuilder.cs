using FuDever.Domain.EntityBuilders.Others;

namespace FuDever.Domain.EntityBuilders.UserPlatform.Others;

/// <summary>
///     Interface for user platform navigation property builder.
/// </summary>
public interface IUserPlatformNavigationPropertyBuilder<TBuilder> :
    IBaseEntityHandler<Entities.UserPlatform>
        where TBuilder : IBaseUserPlatformBuilder
{
    /// <summary>
    ///     Sets platform.
    /// </summary>
    /// <param name="platform">
    ///     The platform.
    /// </param>
    /// <returns>
    ///     The builder.
    /// </returns>
    TBuilder WithPlatform(Entities.Platform platform);
}
