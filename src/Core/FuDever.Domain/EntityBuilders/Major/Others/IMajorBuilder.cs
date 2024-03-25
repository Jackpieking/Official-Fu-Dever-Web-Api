using FuDever.Domain.EntityBuilders.Others;
using System;

namespace FuDever.Domain.EntityBuilders.Major.Others;

/// <summary>
///     Interface for major builder.
/// </summary>
public interface IMajorBuilder<TBuilder> :
    IBaseEntityHandler<Entities.Major>
        where TBuilder : IBaseMajorBuilder
{
    /// <summary>
    ///     Set the id of major.
    /// </summary>
    /// <param name="majorId">
    ///     Id of major.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithId(Guid majorId);

    /// <summary>
    ///     Set the name of major.
    /// </summary>
    /// <param name="majorName">
    ///     Name of major.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithName(string majorName);

    /// <summary>
    ///     Set the remove time of major.
    /// </summary>
    /// <param name="majorRemovedAt">
    ///     Remove time of major.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithRemovedAt(DateTime majorRemovedAt);

    /// <summary>
    ///     Set the remover of major.
    /// </summary>
    /// <param name="majorRemovedBy">
    ///     Remover of major.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithRemovedBy(Guid majorRemovedBy);
}
