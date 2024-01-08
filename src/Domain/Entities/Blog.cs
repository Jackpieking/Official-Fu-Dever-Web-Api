using Domain.Entities.Base;
using System;
using System.Collections.Generic;

namespace Domain.Entities;

/// <summary>
///     Represent the "Blogs" table.
/// </summary>
public sealed class Blog :
    IBaseEntity,
    ICreatedEntity,
    ITemporarilyRemovedEntity,
    IUpdatedEntity
{
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
}