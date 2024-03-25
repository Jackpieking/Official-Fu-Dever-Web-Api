using FuDever.Domain.Entities.Base;
using System;

namespace FuDever.Domain.Entities;

/// <summary>
///     Represent the "Projects" table.
/// </summary>
public class Project :
    IBaseEntity,
    IUpdatedEntity,
    ICreatedEntity
{
    internal Project()
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

    public static class Metadata
    {
        public static class Title
        {
            public const int MaxLength = 100;

            public const int MinLength = 2;
        }
    }
}