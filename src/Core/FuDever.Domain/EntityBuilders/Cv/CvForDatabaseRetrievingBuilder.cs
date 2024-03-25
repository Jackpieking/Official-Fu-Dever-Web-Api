using FuDever.Domain.EntityBuilders.Cv.Others;
using System;

namespace FuDever.Domain.EntityBuilders.Cv;

/// <summary>
///     Hobby for database retrieving builder.
/// </summary>
public sealed class CvForDatabaseRetrievingBuilder :
    Entities.Cv,
    IBaseCvBuilder,
    ICvBuilder<CvForDatabaseRetrievingBuilder>
{
    public Entities.Cv Complete()
    {
        return new()
        {
            Id = Id,
            CreatedAt = CreatedAt,
            CreatedBy = CreatedBy,
            RemovedAt = RemovedAt,
            RemovedBy = RemovedBy,
            StudentCvFileId = StudentCvFileId,
            StudentEmail = StudentEmail,
            StudentFullName = StudentFullName,
            StudentId = StudentId
        };
    }

    public CvForDatabaseRetrievingBuilder WithCreatedAt(DateTime cvCreatedAt)
    {
        CreatedAt = cvCreatedAt;

        return this;
    }

    public CvForDatabaseRetrievingBuilder WithCreatedBy(Guid cvCreateBy)
    {
        CreatedBy = cvCreateBy;

        return this;
    }

    public CvForDatabaseRetrievingBuilder WithId(Guid cvId)
    {
        Id = cvId;

        return this;
    }

    public CvForDatabaseRetrievingBuilder WithRemovedAt(DateTime cvRemovedAt)
    {
        RemovedAt = cvRemovedAt;

        return this;
    }

    public CvForDatabaseRetrievingBuilder WithRemovedBy(Guid cvRemovedBy)
    {
        RemovedBy = cvRemovedBy;

        return this;
    }

    public CvForDatabaseRetrievingBuilder WithStudentCvFileId(string studentCvFileId)
    {
        StudentCvFileId = studentCvFileId;

        return this;
    }

    public CvForDatabaseRetrievingBuilder WithStudentEmail(string studentEmail)
    {
        StudentEmail = studentEmail;

        return this;
    }

    public CvForDatabaseRetrievingBuilder WithStudentFullName(string studentName)
    {
        StudentFullName = studentName;

        return this;
    }

    public CvForDatabaseRetrievingBuilder WithStudentId(string studentId)
    {
        StudentId = studentId;

        return this;
    }
}
