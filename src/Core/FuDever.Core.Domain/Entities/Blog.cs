using FuDever.Domain.Entities.Base;
using System;
using System.Collections.Generic;

namespace FuDever.Domain.Entities;

/// <summary>
///     Represent the "Blogs" table.
/// </summary>
public sealed class Blog :
    IBaseEntity,
    ICreatedEntity,
    ITemporarilyRemovedEntity,
    IUpdatedEntity
{
    private Blog()
    {
    }

    /// <summary>
    ///     Blog id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Blog author id.
    /// </summary>
    public Guid AuthorId { get; set; }

    /// <summary>
    ///     Blog title.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    ///     Blog thumbnail.
    /// </summary>
    public string Thumbnail { get; set; }

    /// <summary>
    ///     Blog upload date.
    /// </summary>
    public DateTime UploadDate { get; set; }

    /// <summary>
    ///     Blog content.
    /// </summary>
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
        /// <summary>
        ///     Title property.
        /// </summary>
        public static class Title
        {
            /// <summary>
            ///     Max value length.
            /// </summary>
            public const int MaxLength = 100;

            /// <summary>
            ///     Min value length.
            /// </summary>
            public const int MinLength = 2;
        }

        /// <summary>
        ///     Thumbnail property.
        /// </summary>
        public static class Thumbnail
        {
            /// <summary>
            ///     Max value length.
            /// </summary>
            public const int MaxLength = 200;

            /// <summary>
            ///     Min value length.
            /// </summary>
            public const int MinLength = 2;
        }
    }
}