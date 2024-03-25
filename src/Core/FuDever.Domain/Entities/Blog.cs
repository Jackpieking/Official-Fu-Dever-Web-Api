using FuDever.Domain.Entities.Base;
using System;
using System.Collections.Generic;

namespace FuDever.Domain.Entities;

/// <summary>
///     Represent the "Blogs" table.
/// </summary>
public class Blog :
    IBaseEntity,
    ICreatedEntity,
    ITemporarilyRemovedEntity,
    IUpdatedEntity
{
    internal Blog()
    {
    }

    public Guid Id { get; set; }

    public Guid AuthorId { get; set; }

    public string Title { get; set; }

    public string Thumbnail { get; set; }

    public DateTime UploadDate { get; set; }

    public string Content { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Guid UpdatedBy { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    // Navigation properties.
    public User User { get; set; }

    // Navigation collections.
    public IEnumerable<BlogTag> BlogTags { get; set; }

    public IEnumerable<BlogComment> BlogComments { get; set; }

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

        public static class Thumbnail
        {
            public const int MaxLength = 200;

            public const int MinLength = 2;
        }
    }
}