using FuDever.Domain.EntityBuilders.Others;
using System;

namespace FuDever.Domain.EntityBuilders.UserHobby.Others;

/// <summary>
///     Interface for user hobby builder.
/// </summary>
public interface IUserHobbyBuilder<TBuilder> :
    IBaseEntityHandler<Entities.UserHobby>
        where TBuilder : IBaseUserHobbyBuilder
{
    /// <summary>
    ///     Set the hobby id of user hobby.
    /// </summary>
    /// <param name="hobbyId">
    ///     Id of hobby.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithHobbyId(Guid hobbyId);

    /// <summary>
    ///     Set the user id of user hobby.
    /// </summary>
    /// <param name="userId">
    ///     Id of user.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithUserId(Guid userId);
}
