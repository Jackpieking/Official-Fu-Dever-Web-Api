using System;
using Domain.Entities.Base;

namespace Domain.Entities;

/// <summary>
///     Represent the "Projects" table.
/// </summary>
public sealed class Project :
    IBaseEntity,
    IUpdatedEntity,
    ICreatedEntity
{
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
}