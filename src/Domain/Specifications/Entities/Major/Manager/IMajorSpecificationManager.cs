using System;

namespace Domain.Specifications.Entities.Major.Manager;

/// <summary>
///
/// </summary>
public interface IMajorSpecificationManager
{
    /// <summary>
    ///
    /// </summary>
    IMajorAsNoTrackingSpecification MajorAsNoTrackingSpecification { get; }

    /// <summary>
    ///
    /// </summary>
    IMajorNotTemporarilyRemovedSpecification MajorNotTemporarilyRemovedSpecification { get; }

    /// <summary>
    ///
    /// </summary>
    IMajorTemporarilyRemovedSpecification MajorTemporarilyRemovedSpecification { get; }

    IMajorByIdSpecification MajorByIdSpecification(Guid majorId);

    IMajorByNameSpecification MajorByNameSpecification(
        string majorName,
        bool isCaseSensitive);

    /// <summary>
    ///
    /// </summary>
    ISelectFieldsFromMajorSpecification SelectFieldsFromMajorSpecification { get; }
}
