using System;
using Domain.Entities.Base;

namespace Domain.Entities;

/// <summary>
///     Represent the "Cvs" table.
/// </summary>
public sealed class Cv :
    IBaseEntity,
    ICreatedEntity,
    ITemporarilyRemovedEntity
{
    /// <summary>
    ///     Cv id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Student full name.
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    ///     Student email.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    ///     Student id.
    /// </summary>
    public string StudentId { get; set; }

    /// <summary>
    ///     Student cv file id.
    /// </summary>
    public string CvFileId { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }
}