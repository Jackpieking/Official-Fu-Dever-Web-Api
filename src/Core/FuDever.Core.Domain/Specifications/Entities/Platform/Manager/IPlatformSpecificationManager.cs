using System;

namespace FuDever.Domain.Specifications.Entities.Platform.Manager;

/// <summary>
///     Represent platform specification manager.
/// </summary>
public interface IPlatformSpecificationManager
{
    /// <summary>
    ///     Platform not temporarily removed specification.
    /// </summary>
    IPlatformNotTemporarilyRemovedSpecification PlatformNotTemporarilyRemovedSpecification { get; }

    /// <summary>
    ///     Platform temporarily removed specification.
    /// </summary>
    IPlatformTemporarilyRemovedSpecification PlatformTemporarilyRemovedSpecification { get; }

    /// <summary>
    ///     Platform as no tracking specification.
    /// </summary>
    IPlatformAsNoTrackingSpecification PlatformAsNoTrackingSpecification { get; }

    /// <summary>
    ///     Select field from "Platforms" table specification.
    /// </summary>
    ISelectFieldsFromPlatformSpecification SelectFieldsFromPlatformSpecification { get; }

    /// <summary>
    ///     Platform by platform id specification.
    /// </summary>
    /// <param name="platformId">
    ///     Platform id for finding platform.
    /// </param>
    /// <returns>
    ///     Specification.
    /// </returns>
    IPlatformByIdSpecification PlatformByIdSpecification(Guid platformId);

    /// <summary>
    ///     Platform by platform name specification.
    /// </summary>
    /// <param name="platformName">
    ///     Platform name for finding platform.
    /// </param>
    /// <param name="isCaseSensitive">
    ///     Does platform name need searching in a sensitive way.
    /// </param>
    /// <returns>
    ///     Specification.
    /// </returns>
    IPlatformByNameSpecification PlatformByNameSpecification(
        string platformName,
        bool isCaseSensitive);
}
