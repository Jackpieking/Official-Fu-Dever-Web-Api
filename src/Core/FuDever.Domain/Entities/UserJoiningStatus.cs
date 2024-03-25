using FuDever.Domain.Entities.Base;
using System;
using System.Collections.Generic;

namespace FuDever.Domain.Entities;

/// <summary>
///     Represent the "UserJoiningStatus" table.
/// </summary>
public class UserJoiningStatus : IBaseEntity
{
    internal UserJoiningStatus()
    {
    }

    public Guid Id { get; set; }

    public string Type { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    // Navigation collections.
    public IEnumerable<User> Users { get; set; }

    public static class Metadata
    {
        public static class Type
        {
            public const int MaxLength = 100;

            public const int MinLength = 2;
        }
    }
}