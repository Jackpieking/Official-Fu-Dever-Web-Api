using FuDever.Domain.Entities.Base;
using System;

namespace FuDever.Domain.Entities;

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

    public Guid Id { get; set; }

    public Guid AuthorId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string SourceCodeUrl { get; set; }

    public string DemoUrl { get; set; }

    public string ThumbnailUrl { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid UpdatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    // Navigation properties.
    public User User { get; set; }

    public static Project InitFromDatabaseVer1(
        Guid projectId,
        string projectTitle,
        string projectDescription,
        string projectSourceCodeUrl,
        string projectDemoUrl,
        string projectThumbnailUrl,
        DateTime projectCreatedAt,
        DateTime projectUpdatedAt)
    {
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

    public static Project InitFromDatabaseVer2(
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
        public static class Title
        {
            public const int MaxLength = 100;

            public const int MinLength = 2;
        }
    }
}