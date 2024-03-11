using Domain.Entities.Base;
using System;
using System.Collections.Generic;

namespace Domain.Entities;

/// <summary>
///     Represent the "UserJoiningStatus" table.
/// </summary>
public sealed class UserJoiningStatus : IBaseEntity
{
    private UserJoiningStatus()
    {
    }

    /// <summary>
    ///     User joining status id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     User joining status type.
    /// </summary>
    public string Type { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    // Navigation collections.
    public IEnumerable<User> Users { get; set; }

    /// <summary>
    ///     Return an instance.
    /// </summary>
    /// <param name="userJoiningStatusId">
    ///     Id of user joining status.
    /// </param>
    /// <param name="userJoiningStatusType">
    ///     User joining status name.
    /// </param>
    /// <param name="userJoiningStatusRemovedAt">
    ///     User joining status is removed by whom.
    /// </param>
    /// <param name="userJoiningStatusRemovedBy">
    ///     When is user joining status removed.
    /// </param>
    /// <returns>
    ///     A new user joining status object.
    /// </returns>
    public static UserJoiningStatus InitVer1(
        Guid userJoiningStatusId,
        string userJoiningStatusType,
        DateTime userJoiningStatusRemovedAt,
        Guid userJoiningStatusRemovedBy)
    {
        // Validate user joining status type.
        if (string.IsNullOrWhiteSpace(value: userJoiningStatusType) ||
            userJoiningStatusType.Length > Metadata.Type.MaxLength ||
            userJoiningStatusType.Length < Metadata.Type.MinLength)
        {
            return default;
        }

        // Validate user joining status Id.
        if (userJoiningStatusId == Guid.Empty)
        {
            return default;
        }

        // Validate user joining status removed by.
        if (userJoiningStatusRemovedBy == Guid.Empty)
        {
            return default;
        }

        return new()
        {
            Id = userJoiningStatusId,
            Type = userJoiningStatusType,
            RemovedAt = userJoiningStatusRemovedAt,
            RemovedBy = userJoiningStatusRemovedBy
        };
    }

    /// <summary>
    ///     Return an instance.
    /// </summary>
    /// <param name="userJoiningStatusId">
    ///     Id of user joining status.
    /// </param>
    /// <returns>
    ///     A new user joining status object.
    /// </returns>
    public static UserJoiningStatus InitVer2(Guid userJoiningStatusId)
    {
        // Validate skill Id.
        if (userJoiningStatusId == Guid.Empty)
        {
            return default;
        }

        return new()
        {
            Id = userJoiningStatusId
        };
    }

    /// <summary>
    ///     Return an instance.
    /// </summary>
    /// <param name="userJoiningStatusType">
    ///     User joining status name.
    /// </param>
    /// <returns>
    ///     A new user joining status object.
    /// </returns>
    public static UserJoiningStatus InitVer3(string userJoiningStatusType)
    {
        // Validate user joining status type.
        if (string.IsNullOrWhiteSpace(value: userJoiningStatusType) ||
            userJoiningStatusType.Length > Metadata.Type.MaxLength ||
            userJoiningStatusType.Length < Metadata.Type.MinLength)
        {
            return default;
        }

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
        /// <summary>
        ///     Type property.
        /// </summary>
        public static class Type
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
    }
}