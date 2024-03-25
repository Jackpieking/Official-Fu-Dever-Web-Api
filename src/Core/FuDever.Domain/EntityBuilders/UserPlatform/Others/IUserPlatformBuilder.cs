using FuDever.Domain.EntityBuilders.Others;
using System;

namespace FuDever.Domain.EntityBuilders.UserPlatform.Others;

/// <summary>
///     Interface for user platform builder.
/// </summary>
public interface IUserPlatformBuilder<TBuilder> :
    IBaseEntityHandler<Entities.UserPlatform>
        where TBuilder : IBaseUserPlatformBuilder
{
    /// <summary>
    ///     Set the platform id of user platform.
    /// </summary>
    /// <param name="platformId">
    ///     Id of platform.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithPlatformId(Guid platformId);

    /// <summary>
    ///     Set the user id of user platform.
    /// </summary>
    /// <param name="userId">
    ///     Id of user.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithUserId(Guid userId);

    /// <summary>
    ///     Set the platform url of user platform.
    /// </summary>
    /// <param name="platformUrl">
    ///     Url of platform.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithPlatformUrl(string platformUrl);
}
