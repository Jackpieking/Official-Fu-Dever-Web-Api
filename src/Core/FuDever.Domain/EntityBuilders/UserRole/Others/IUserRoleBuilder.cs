using FuDever.Domain.EntityBuilders.Others;
using System;

namespace FuDever.Domain.EntityBuilders.UserRole.Others;

/// <summary>
///     Interface for user role builder.
/// </summary>
public interface IUserRoleBuilder<TBuilder> :
    IBaseEntityHandler<Entities.UserRole>
        where TBuilder : IBaseUserRoleBuilder
{
    /// <summary>
    ///     Set the user id.
    /// </summary>
    /// <param name="userId">
    ///     The user id.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithUserId(Guid userId);

    /// <summary>
    ///     Set the role id.
    /// </summary>
    /// <param name="roleId">
    ///     The role id.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithRoleId(Guid roleId);
}
