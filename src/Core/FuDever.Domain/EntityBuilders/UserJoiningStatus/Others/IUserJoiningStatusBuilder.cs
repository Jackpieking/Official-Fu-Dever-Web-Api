using FuDever.Domain.EntityBuilders.Others;
using System;

namespace FuDever.Domain.EntityBuilders.UserJoiningStatus.Others;

/// <summary>
///     Interface for user joining status builder.
/// </summary>
public interface IUserJoiningStatusBuilder<TBuilder> :
    IBaseEntityHandler<Entities.UserJoiningStatus>
        where TBuilder : IBaseUserJoiningStatusBuilder
{
    /// <summary>
    ///     Set the id of user joining status.
    /// </summary>
    /// <param name="userJoiningStatusId">
    ///     Id of user joining status.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithId(Guid userJoiningStatusId);

    /// <summary>
    ///     Set the type of user joining status.
    /// </summary>
    /// <param name="userJoiningStatusType">
    ///     Type of user joining status.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithType(string userJoiningStatusType);

    /// <summary>
    ///     Set the remove time of user joining status.
    /// </summary>
    /// <param name="userJoiningStatusRemovedAt">
    ///     Remove time of user joining status.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithRemovedAt(DateTime userJoiningStatusRemovedAt);

    /// <summary>
    ///     Set the remover of user joining status.
    /// </summary>
    /// <param name="userJoiningStatusRemovedBy">
    ///     Remover of user joining status.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithRemovedBy(Guid userJoiningStatusRemovedBy);
}
