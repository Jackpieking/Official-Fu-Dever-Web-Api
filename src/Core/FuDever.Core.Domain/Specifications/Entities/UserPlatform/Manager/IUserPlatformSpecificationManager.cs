using System;

namespace FuDever.Domain.Specifications.Entities.UserPlatform.Manager;

/// <summary>
///     Represent user platform specification manager.
/// </summary>
public interface IUserPlatformSpecificationManager
{
    /// <summary>
    ///     User platform by platform id specification.
    /// </summary>
    /// <param name="platformId">
    ///     Platform id for finding user platform.
    /// </param>
    /// <returns>
    ///     Specification.
    /// </returns>
    IUserPlatformByPlatformIdSpecification UserPlatformByPlatformIdSpecification(Guid platformId);

    /// <summary>
    ///     User platform by user id specification.
    /// </summary>
    /// <param name="userId">
    ///     User id for finding user platform.
    /// </param>
    /// <returns>
    ///     Specification.
    /// </returns>
    IUserPlatformByUserIdSpecification UserPlatformByUserIdSpecification(Guid userId);

    /// <summary>
    ///     Select field from "UserPlatforms" table specification.
    /// </summary>
    ISelectFieldsFromUserPlatformSpecification SelectFieldsFromUserPlatformSpecification { get; }

    /// <summary>
    ///     User platform as no tracking specification.
    /// </summary>
    IUserPlatformAsNoTrackingSpecification UserPlatformAsNoTrackingSpecification { get; }
}
