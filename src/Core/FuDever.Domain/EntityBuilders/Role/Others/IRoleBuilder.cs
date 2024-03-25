using FuDever.Domain.EntityBuilders.Others;
using System;

namespace FuDever.Domain.EntityBuilders.Role.Others;

/// <summary>
///     Interface for role builder.
/// </summary>
public interface IRoleBuilder<TBuilder> :
    IBaseEntityHandler<Entities.Role>
        where TBuilder : IBaseRoleBuilder
{
    /// <summary>
    ///     Set role id.
    /// </summary>
    /// <param name="roleId">
    ///     The role id.
    /// </param>
    /// returns>
    ///     Itself.
    /// </returns>
    TBuilder WithId(Guid roleId);

    /// <summary>
    ///     Set role name.
    /// </summary>
    /// <param name="roleName">
    ///     The role name.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithName(string roleName);

    /// <summary>
    ///     Set role normalized name.
    /// </summary>
    /// <param name="roleNormalizedName">
    ///     The role normalized name.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithNormalizedName(string roleNormalizedName);

    /// <summary>
    ///     Set role concurrency stamp.
    /// </summary>
    /// <param name="roleConcurrencyStamp">
    ///     The role concurrency stamp.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithConcurrencyStamp(string roleConcurrencyStamp);

    /// <summary>
    ///     Set role removed at.
    /// </summary>
    /// <param name="roleRemovedAt">
    ///     The role removed at.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithRemovedAt(DateTime roleRemovedAt);

    /// <summary>
    ///     Set role removed by.
    /// </summary>
    /// <param name="roleRemovedBy">
    ///     The role removed by.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithRemovedBy(Guid roleRemovedBy);
}
