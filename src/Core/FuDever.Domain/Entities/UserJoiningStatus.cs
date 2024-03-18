using FuDever.Domain.Entities.Base;
using System;
using System.Collections.Generic;

namespace FuDever.Domain.Entities;

/// <summary>
///     Represent the "UserJoiningStatus" table.
/// </summary>
public sealed class UserJoiningStatus : IBaseEntity
{
    private UserJoiningStatus()
    {
    }

    public Guid Id { get; set; }

    public string Type { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    // Navigation collections.
    public IEnumerable<User> Users { get; set; }

    public static UserJoiningStatus InitForSeeding(
        Guid userJoiningStatusId,
        string userJoiningStatusType,
        DateTime userJoiningStatusRemovedAt,
        Guid userJoiningStatusRemovedBy)
    {
        return new()
        {
            Id = userJoiningStatusId,
            Type = userJoiningStatusType,
            RemovedAt = userJoiningStatusRemovedAt,
            RemovedBy = userJoiningStatusRemovedBy
        };
    }

    public static UserJoiningStatus InitFromDatabaseVer1(Guid userJoiningStatusId)
    {
        return new()
        {
            Id = userJoiningStatusId
        };
    }

    public static UserJoiningStatus InitFromDatabaseVer2(string userJoiningStatusType)
    {
        return new()
        {
            Type = userJoiningStatusType
        };
    }

    /// <summary>
    ///     Represent metadata of property.
    /// </summary>
    public static class Metadata
    {
        public static class Type
        {
            public const int MaxLength = 100;

            public const int MinLength = 2;
        }
    }
}