using System;

namespace FuDever.Domain.Specifications.Entities.Major.Manager;

/// <summary>
///     Represent major specification manager.
/// </summary>
public interface IMajorSpecificationManager
{
    /// <summary>
    ///     Major as no tracking specification.
    /// </summary>
    IMajorAsNoTrackingSpecification MajorAsNoTrackingSpecification { get; }

    /// <summary>
    ///     Major not temporarily removed specification.
    /// </summary>
    IMajorNotTemporarilyRemovedSpecification MajorNotTemporarilyRemovedSpecification { get; }

    /// <summary>
    ///     Major temporarily removed specification.
    /// </summary>
    IMajorTemporarilyRemovedSpecification MajorTemporarilyRemovedSpecification { get; }

    /// <summary>
    ///     Major by major id specification.
    /// </summary>
    /// <param name="majorId">
    ///     Major id for finding major.
    /// </param>
    /// <returns>
    ///     Specification.
    /// </returns>
    IMajorByIdSpecification MajorByIdSpecification(Guid majorId);

    /// <summary>
    ///     Major by major name specification.
    /// </summary>
    /// <param name="majorName">
    ///     Major name for finding major.
    /// </param>
    /// <param name="isCaseSensitive">
    ///     Does major name need searching in a sensitive way.
    /// </param>
    /// <returns>
    ///     Specification.
    /// </returns>
    IMajorByNameSpecification MajorByNameSpecification(
        string majorName,
        bool isCaseSensitive);

    /// <summary>
    ///     Select field from "Majors" table specification.
    /// </summary>
    ISelectFieldsFromMajorSpecification SelectFieldsFromMajorSpecification { get; }

    /// <summary>
    ///     Update field of major specification.
    /// </summary>
    IUpdateFieldOfMajorSpecification UpdateFieldOfMajorSpecification { get; }

    /// <summary>
    ///     Major name is not default specification.
    /// </summary>
    IMajorNameIsNotDefaultSpecification MajorNameIsNotDefaultSpecification { get; }
}
