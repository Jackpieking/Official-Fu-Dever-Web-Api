using FuDever.Domain.Entities.Base;
using System;

namespace FuDever.Domain.Entities;

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

    public Guid Id { get; set; }

    public Guid BlogId { get; set; }

    public Guid AuthorId { get; set; }

    public DateTime UploadDate { get; set; }

    public string Content { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Guid UpdatedBy { get; set; }

    // Navigation properties.
    public Blog Blog { get; set; }

    public User User { get; set; }
}