using Domain.Entities.Base;
using System;

namespace Domain.Entities;

/// <summary>
///     Represent the "BLogComments" table.
/// </summary>
public sealed class BlogComment :
    IBaseEntity,
    ICreatedEntity,
    IUpdatedEntity
{
    private BlogComment()
    {
    }

    /// <summary>
    ///     Comment id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Blog id.
    /// </summary>
    public Guid BlogId { get; set; }

    /// <summary>
    ///     Comment author id.
    /// </summary>
    public Guid AuthorId { get; set; }

    /// <summary>
    ///     Comment upload date.
    /// </summary>
    public DateTime UploadDate { get; set; }

    /// <summary>
    ///     Comment content.
    /// </summary>
    public string Content { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Guid UpdatedBy { get; set; }

    // Navigation properties.
    public Blog Blog { get; set; }

    public User User { get; set; }
}