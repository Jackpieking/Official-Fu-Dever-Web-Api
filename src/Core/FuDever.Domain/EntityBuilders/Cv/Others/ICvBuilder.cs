using FuDever.Domain.EntityBuilders.Others;
using System;

namespace FuDever.Domain.EntityBuilders.Cv.Others;

/// <summary>
///     Interface for cv builder.
/// </summary>
public interface ICvBuilder<TBuilder> :
    IBaseEntityHandler<Entities.Cv>
        where TBuilder : IBaseCvBuilder
{
    /// <summary>
    ///     Set cv id.
    /// </summary>
    /// <param name="cvId">
    ///     Id of cv.
    /// </param>
    /// <returns>
    ///     Cv builder.
    /// </returns>
    TBuilder WithId(Guid cvId);

    /// <summary>
    ///     Set student full name.
    /// </summary>
    /// <param name="studentName">
    ///     Name of student.
    /// </param>
    /// <returns>
    ///     Cv builder.
    /// </returns>
    TBuilder WithStudentFullName(string studentName);

    /// <summary>
    ///     Set student email.
    /// </summary>
    /// <param name="studentEmail">
    ///     Email of student.
    /// </param>
    /// <returns>
    ///     Cv builder.
    /// </returns>
    TBuilder WithStudentEmail(string studentEmail);

    /// <summary>
    ///     Set student id.
    /// </summary>
    /// <param name="studentId">
    ///     Id of student.
    /// </param>
    /// <returns>
    ///     Cv builder.
    /// </returns>
    TBuilder WithStudentId(string studentId);

    /// <summary>
    ///     Set student cv file id.
    /// </summary>
    /// <param name="studentCvFileId">
    ///     Id of student cv file.
    /// </param>
    /// <returns>
    ///     Cv builder.
    /// </returns>
    TBuilder WithStudentCvFileId(string studentCvFileId);

    /// <summary>
    ///     Set cv created time.
    /// </summary>
    /// <param name="cvCreatedAt">
    ///     Created time of cv.
    /// </param>
    /// <returns>
    ///     Cv builder.
    /// </returns>
    TBuilder WithCreatedAt(DateTime cvCreatedAt);

    /// <summary>
    ///     Set cv creator.
    /// </summary>
    /// <param name="cvCreateBy">
    ///     Id of cv creator.
    /// </param>
    /// <returns>
    ///     Cv builder.
    /// </returns>
    TBuilder WithCreatedBy(Guid cvCreateBy);

    /// <summary>
    ///     Set cv removed time.
    /// </summary>
    /// <param name="cvRemovedAt">
    ///     Removed time of cv.
    /// </param>
    /// <returns>
    ///     Cv builder.
    /// </returns>
    TBuilder WithRemovedAt(DateTime cvRemovedAt);

    /// <summary>
    ///     Set cv remover.
    /// </summary>
    /// <param name="cvRemovedBy">
    ///     Id of cv remover.
    /// </param>
    /// <returns>
    ///     Cv builder.
    /// </returns>
    TBuilder WithRemovedBy(Guid cvRemovedBy);
}
