using FuDever.Domain.EntityBuilders.Project.Others;
using System;

namespace FuDever.Domain.EntityBuilders.Project;

/// <summary>
///     Project for database retrieving builder.
/// </summary>
public sealed class ProjectForDatabaseRetrievingBuilder :
    Entities.Project,
    IBaseProjectBuilder,
    IProjectBuilder<ProjectForDatabaseRetrievingBuilder>
{
    public Entities.Project Complete()
    {
        return new()
        {
            Id = Id,
            AuthorId = AuthorId,
            Title = Title,
            Description = Description,
            SourceCodeUrl = SourceCodeUrl,
            DemoUrl = DemoUrl,
            ThumbnailUrl = ThumbnailUrl,
            CreatedBy = CreatedBy,
            CreatedAt = CreatedAt,
            UpdatedBy = UpdatedBy,
            UpdatedAt = UpdatedAt
        };
    }

    public ProjectForDatabaseRetrievingBuilder WithId(Guid projectId)
    {
        Id = projectId;

        return this;
    }

    public ProjectForDatabaseRetrievingBuilder WithAuthorId(Guid projectAuthorId)
    {
        AuthorId = projectAuthorId;

        return this;
    }

    public ProjectForDatabaseRetrievingBuilder WithTitle(string projectTitle)
    {
        Title = projectTitle;

        return this;
    }

    public ProjectForDatabaseRetrievingBuilder WithDescription(string projectDescription)
    {
        Description = projectDescription;

        return this;
    }

    public ProjectForDatabaseRetrievingBuilder WithSourceCodeUrl(string projectSourceCodeUrl)
    {
        SourceCodeUrl = projectSourceCodeUrl;

        return this;
    }

    public ProjectForDatabaseRetrievingBuilder WithDemoUrl(string projectDemoUrl)
    {
        DemoUrl = projectDemoUrl;

        return this;
    }

    public ProjectForDatabaseRetrievingBuilder WithThumbnailUrl(string projectThumbnailUrl)
    {
        ThumbnailUrl = projectThumbnailUrl;

        return this;
    }

    public ProjectForDatabaseRetrievingBuilder WithCreatedBy(Guid projectCreatedBy)
    {
        CreatedBy = projectCreatedBy;

        return this;
    }

    public ProjectForDatabaseRetrievingBuilder WithCreatedAt(DateTime projectCreatedAt)
    {
        CreatedAt = projectCreatedAt;

        return this;
    }

    public ProjectForDatabaseRetrievingBuilder WithUpdatedBy(Guid projectUpdatedBy)
    {
        UpdatedBy = projectUpdatedBy;

        return this;
    }

    public ProjectForDatabaseRetrievingBuilder WithUpdatedAt(DateTime projectUpdatedAt)
    {
        UpdatedAt = projectUpdatedAt;

        return this;
    }
}
