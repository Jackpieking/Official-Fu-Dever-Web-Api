using FuDever.Domain.Entities.Base;
using System;
using System.Collections.Generic;

namespace FuDever.Domain.Entities;

/// <summary>
///     Represent the "Departments" table.
/// </summary>
public class Department :
    IBaseEntity,
    ITemporarilyRemovedEntity
{
    internal Department()
    {
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    // Navigation collections.
    public IEnumerable<User> Users { get; set; }

    public static class Metadata
    {
        public static class Name
        {
            public const int MaxLength = 100;

            public const int MinLength = 2;
        }
    }
}