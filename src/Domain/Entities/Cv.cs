using Domain.Entities.Base;
using System;

namespace Domain.Entities;

/// <summary>
///     Represent the "Cvs" table.
/// </summary>
public sealed class Cv :
    IBaseEntity,
    ICreatedEntity,
    ITemporarilyRemovedEntity
{
    private Cv()
    {
    }

    /// <summary>
    ///     Cv id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Student full name.
    /// </summary>
    public string StudentFullName { get; set; }

    /// <summary>
    ///     Student email.
    /// </summary>
    public string StudentEmail { get; set; }

    /// <summary>
    ///     Student id.
    /// </summary>
    public string StudentId { get; set; }

    /// <summary>
    ///     Student cv file id.
    /// </summary>
    public string StudentCvFileId { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    /// <summary>
    ///     Return an instance.
    /// </summary>
    /// <param name="cvId">
    ///     Id of cv.
    /// </param>
    /// <param name="studentFullName">
    ///     Student full name.
    /// </param>
    /// <param name="studentEmail">
    ///     Student email.
    /// </param>
    /// <param name="studentId">
    ///     Id of student.
    /// </param>
    /// <param name="studentCvFieldId">
    ///     Id of cv file.
    /// </param>
    /// <returns>
    ///     A new cv object.
    /// </returns>
    public static Cv InitVer1(
        Guid cvId,
        string studentFullName,
        string studentEmail,
        string studentId,
        string studentCvFieldId)
    {
        // Validate cv id.
        if (cvId == Guid.Empty)
        {
            return default;
        }

        // Validate cv student full name name.
        if (string.IsNullOrWhiteSpace(value: studentFullName) ||
            studentFullName.Length > Metadata.StudentFullName.MaxLength ||
            studentFullName.Length < Metadata.StudentFullName.MinLength)
        {
            return default;
        }

        // Validate cv student email.
        if (string.IsNullOrWhiteSpace(value: studentEmail))
        {
            return default;
        }

        // Validate cv student id.
        if (string.IsNullOrWhiteSpace(value: studentId) ||
            studentId.Length > Metadata.StudentId.MaxLength ||
            studentId.Length < Metadata.StudentId.MinLength)
        {
            return default;
        }

        // Validate cv file id.
        if (string.IsNullOrWhiteSpace(value: studentCvFieldId))
        {
            return default;
        }

        return new()
        {
            Id = cvId,
            StudentFullName = studentFullName,
            StudentEmail = studentEmail,
            StudentId = studentId,
            StudentCvFileId = studentCvFieldId
        };
    }

    /// <summary>
    ///     Represent requirement of property.
    /// </summary>
    public static class Metadata
    {
        /// <summary>
        ///     Student full name property.
        /// </summary>
        public static class StudentFullName
        {
            /// <summary>
            ///     Max value length.
            /// </summary>
            public const int MaxLength = 50;

            /// <summary>
            ///     Min value length.
            /// </summary>
            public const int MinLength = 2;
        }

        /// <summary>
        ///     Student id property.
        /// </summary>
        public static class StudentId
        {
            /// <summary>
            ///     Max value length.
            /// </summary>
            public const int MaxLength = 10;

            /// <summary>
            ///     Min value length.
            /// </summary>
            public const int MinLength = 2;
        }
    }
}