using FuDever.Domain.EntityBuilders.Others;
using System;

namespace FuDever.Domain.EntityBuilders.Platform.Others;

/// <summary>
///     Interface for platform builder.
/// </summary>
public interface IPlatformBuilder<TBuilder> :
    IBaseEntityHandler<Entities.Platform>
        where TBuilder : IBasePlatformBuilder
{
    /// <summary>
    ///     Set the id of platform.
    /// </summary>
    /// <param name="platformId">
    ///     Id of platform.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithId(Guid platformId);

    /// <summary>
    ///     Set the name of platform.
    /// </summary>
    /// <param name="platformName">
    ///     Name of platform.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithName(string platformName);

    /// <summary>
    ///     Set the remove time of platform.
    /// </summary>
    /// <param name="platformRemovedAt">
    ///     Remove time of platform.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithRemovedAt(DateTime platformRemovedAt);

    /// <summary>
    ///     Set the remover of platform.
    /// </summary>
    /// <param name="platformRemovedBy">
    ///     Remover of platform.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithRemovedBy(Guid platformRemovedBy);
}
