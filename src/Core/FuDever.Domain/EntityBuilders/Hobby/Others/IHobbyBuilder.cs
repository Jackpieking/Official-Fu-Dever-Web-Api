using FuDever.Domain.EntityBuilders.Others;
using System;

namespace FuDever.Domain.EntityBuilders.Hobby.Others;

/// <summary>
///     Interface for hobby builder.
/// </summary>
public interface IHobbyBuilder<TBuilder> :
    IBaseEntityHandler<Entities.Hobby>
        where TBuilder : IBaseHobbyBuilder
{
    /// <summary>
    ///     Set the id of hobby.
    /// </summary>
    /// <param name="hobbyId">
    ///     Id of hobby.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithId(Guid hobbyId);

    /// <summary>
    ///     Set the name of hobby.
    /// </summary>
    /// <param name="hobbyName">
    ///     Name of hobby.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithName(string hobbyName);

    /// <summary>
    ///     Set the remove time of hobby.
    /// </summary>
    /// <param name="hobbyRemovedAt">
    ///     Remove time of hobby.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithRemovedAt(DateTime hobbyRemovedAt);

    /// <summary>
    ///     Set the remover of hobby.
    /// </summary>
    /// <param name="hobbyRemovedBy">
    ///     Remover of hobby.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithRemovedBy(Guid hobbyRemovedBy);
}
