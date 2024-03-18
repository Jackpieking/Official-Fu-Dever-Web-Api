using FuDever.Domain.Entities.Base;
using System;

namespace FuDever.Domain.Entities;

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

    public Guid Id { get; set; }

    public string StudentFullName { get; set; }

    public string StudentEmail { get; set; }

    public string StudentId { get; set; }

    public string StudentCvFileId { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    public static Cv InitFromDatabaseVer1(
        Guid cvId,
        string studentFullName,
        string studentEmail,
        string studentId,
        string studentCvFieldId)
    {
        return new()
        {
            Id = cvId,
            StudentFullName = studentFullName,
            StudentEmail = studentEmail,
            StudentId = studentId,
            StudentCvFileId = studentCvFieldId
        };
    }

    public static class Metadata
    {
        public static class StudentFullName
        {
            public const int MaxLength = 50;

            public const int MinLength = 2;
        }

        public static class StudentId
        {
            public const int MaxLength = 10;

            public const int MinLength = 2;
        }
    }
}