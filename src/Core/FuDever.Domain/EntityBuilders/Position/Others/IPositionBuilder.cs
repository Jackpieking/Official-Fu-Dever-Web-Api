using FuDever.Domain.EntityBuilders.Others;
using System;

namespace FuDever.Domain.EntityBuilders.Position.Others;

/// <summary>
///     Interface for position builder.
/// </summary>
public interface IPositionBuilder<TBuilder> :
    IBaseEntityHandler<Entities.Position>
        where TBuilder : IBasePositionBuilder
{
    /// <summary>
    ///     Set the id of position.
    /// </summary>
    /// <param name="positionId">
    ///     Id of position.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithId(Guid positionId);

    /// <summary>
    ///     Set the name of position.
    /// </summary>
    /// <param name="positionName">
    ///     Name of position.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithName(string positionName);

    /// <summary>
    ///     Set the remove time of position.
    /// </summary>
    /// <param name="positionRemovedAt">
    ///     Remove time of position.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithRemovedAt(DateTime positionRemovedAt);

    /// <summary>
    ///     Set the remover of position.
    /// </summary>
    /// <param name="positionRemovedBy">
    ///     Remover of position.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithRemovedBy(Guid positionRemovedBy);
}
