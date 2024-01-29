using System;
using Application.Interfaces.Messaging;

namespace Application.Features.User.Queries.IsUserApproved;

/// <summary>
///     Is user approved query model.
/// </summary>
public sealed class IsUserApprovedQuery : IQuery<bool>
{
    /// <summary>
    ///     User id of user.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    ///     How long the result should live in cache.
    /// </summary>
    /// <remarks>
    ///     Don't give this property a value when don't
    ///     want the result to be cached.
    /// </remarks>
    public int CacheExpiredTime { get; set; } = default;
}
