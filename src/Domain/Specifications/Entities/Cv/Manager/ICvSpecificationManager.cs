using System;

namespace Domain.Specifications.Entities.Cv.Manager;

/// <summary>
///     Represent cv specification manager.
/// </summary>
public interface ICvSpecificationManager
{
    /// <summary>
    ///     Cv by student email specification.
    /// </summary>
    /// <param name="email">
    ///     Student email for finding cv.
    /// </param>
    /// <returns>
    ///     Specification.
    /// </returns>
    ICvByEmailSpecification CvByEmailSpecification(string email);

    /// <summary>
    ///     Cv by student id specification.
    /// </summary>
    /// <param name="studentId">
    ///     Student id for finding cv.
    /// </param>
    /// <returns>
    ///     Specification.
    /// </returns>
    ICvByStudentIdSpecification CvByStudentIdSpecification(string studentId);

    /// <summary>
    ///     Cv by cv id specification.
    /// </summary>
    /// <param name="cvId">
    ///     Cv id for finding cv.
    /// </param>
    /// <returns>
    ///     Specification.
    /// </returns>
    ICvByIdSpecification CvByIdSpecification(Guid cvId);

    /// <summary>
    ///     Cv as no tracking specification.
    /// </summary>
    ICvAsNoTrackingSpecification CvAsNoTrackingSpecification { get; }

    /// <summary>
    ///     Select field from "Cvs" table specification
    /// </summary>
    ISelectFieldsFromCvSpecification SelectFieldsFromCvSpecification { get; }
}
