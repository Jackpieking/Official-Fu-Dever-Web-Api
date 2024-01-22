using Domain.Entities.Base;
using System;

namespace Domain.Entities;

/// <summary>
///     Represent the "Projects" table.
/// </summary>
public sealed class Project :
    IBaseEntity,
    IUpdatedEntity,
    ICreatedEntity
{
    private Project()
    {
    }

    /// <summary>
    ///     Project id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Project author id.
    /// </summary>
    public Guid AuthorId { get; set; }

    /// <summary>
    ///     Project title.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    ///     Project description.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    ///     Project source code url.
    /// </summary>
    public string SourceCodeUrl { get; set; }

    /// <summary>
    ///     Project live demo url.
    /// </summary>
    public string DemoUrl { get; set; }

    /// <summary>
    ///     Project thumbnail url.
    /// </summary>
    public string ThumbnailUrl { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid UpdatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    // Navigation properties.
    public User User { get; set; }

    /// <summary>
    ///     Return an instance.
    /// </summary>
    /// <param name="projectId">
    ///     Id of project.
    /// </param>
    /// <param name="projectTitle">
    ///     Project title.
    /// </param>
    /// <param name="projectDescription">
    ///     Project description.
    /// </param>
    /// <param name="projectSourceCodeUrl">
    ///     Project source code url.
    /// </param>
    /// <param name="projectDemoUrl">
    ///     Project demo url.
    /// </param>
    /// <param name="projectThumbnailUrl">
    ///     Project thumbnail url.
    /// </param>
    /// <param name="projectCreatedAt">
    ///     When is project created.
    /// </param>
    /// <param name="projectUpdatedAt">
    ///     When is project updated.
    /// </param>
    /// <returns>
    ///     A new project object.
    /// </returns>
    public static Project Init(
        Guid projectId,
        string projectTitle,
        string projectDescription,
        string projectSourceCodeUrl,
        string projectDemoUrl,
        string projectThumbnailUrl,
        DateTime projectCreatedAt,
        DateTime projectUpdatedAt)
    {
        // Validate project Id.
        if (projectId == Guid.Empty)
        {
            return default;
        }

        // Validate project title.
        if (string.IsNullOrWhiteSpace(value: projectTitle) ||
            projectTitle.Length > Metadata.Title.MaxLength)
        {
            return default;
        }

        // Validate project description.
        if (string.IsNullOrWhiteSpace(value: projectDescription))
        {
            return default;
        }

        // Validate project source code url.
        if (string.IsNullOrWhiteSpace(value: projectSourceCodeUrl))
        {
            return default;
        }

        // Validate project demo url.
        if (string.IsNullOrWhiteSpace(value: projectDemoUrl))
        {
            return default;
        }

        // Validate project thumbnail url.
        if (string.IsNullOrWhiteSpace(value: projectThumbnailUrl))
        {
            return default;
        }

        return new()
        {
            Id = projectId,
            Title = projectTitle,
            Description = projectDescription,
            SourceCodeUrl = projectSourceCodeUrl,
            DemoUrl = projectDemoUrl,
            ThumbnailUrl = projectThumbnailUrl,
            CreatedAt = projectCreatedAt,
            UpdatedAt = projectUpdatedAt
        };
    }

    /// <summary>
    ///     Return an instance.
    /// </summary>
    /// <param name="projectId">
    ///     Id of project.
    /// </param>
    /// <param name="projectTitle">
    ///     Project title.
    /// </param>
    /// <param name="projectTitle">
    ///     Author id of project.
    /// </param>
    /// <param name="projectDescription">
    ///     Project description.
    /// </param>
    /// <param name="projectSourceCodeUrl">
    ///     Project source code url.
    /// </param>
    /// <param name="projectDemoUrl">
    ///     Project demo url.
    /// </param>
    /// <param name="projectThumbnailUrl">
    ///     Project thumbnail url.
    /// </param>
    /// <param name="projectCreatedAt">
    ///     When is project created.
    /// </param>
    /// <param name="projectUpdatedAt">
    ///     When is project updated.
    /// </param>
    /// <returns>
    ///     A new project object.
    /// </returns>
    public static Project Init(
        Guid projectId,
        string projectTitle,
        Guid projectAuthorId,
        string projectDescription,
        string projectSourceCodeUrl,
        string projectDemoUrl,
        string projectThumbnailUrl,
        DateTime projectCreatedAt,
        DateTime projectUpdatedAt)
    {
        // Validate project Id.
        if (projectId == Guid.Empty)
        {
            return default;
        }

        // Validate project title.
        if (string.IsNullOrWhiteSpace(value: projectTitle) ||
            projectTitle.Length > Metadata.Title.MaxLength)
        {
            return default;
        }

        // Validate project Id.
        if (projectAuthorId == Guid.Empty)
        {
            return default;
        }

        // Validate project description.
        if (string.IsNullOrWhiteSpace(value: projectDescription))
        {
            return default;
        }

        // Validate project source code url.
        if (string.IsNullOrWhiteSpace(value: projectSourceCodeUrl))
        {
            return default;
        }

        // Validate project demo url.
        if (string.IsNullOrWhiteSpace(value: projectDemoUrl))
        {
            return default;
        }

        // Validate project thumbnail url.
        if (string.IsNullOrWhiteSpace(value: projectThumbnailUrl))
        {
            return default;
        }

        return new()
        {
            Id = projectId,
            Title = projectTitle,
            AuthorId = projectAuthorId,
            Description = projectDescription,
            SourceCodeUrl = projectSourceCodeUrl,
            DemoUrl = projectDemoUrl,
            ThumbnailUrl = projectThumbnailUrl,
            CreatedAt = projectCreatedAt,
            UpdatedAt = projectUpdatedAt
        };
    }

    /// <summary>
    ///     Represent metadata of property.
    /// </summary>
    public static class Metadata
    {
        /// <summary>
        ///     Title property.
        /// </summary>
        public static class Title
        {
            /// <summary>
            ///     Max value length.
            /// </summary>
            public const int MaxLength = 100;
        }
    }
}