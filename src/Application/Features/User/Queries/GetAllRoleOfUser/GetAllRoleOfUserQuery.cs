using System.Collections.Generic;
using Application.Interfaces.Messaging;

namespace Application.Features.User.Queries.GetAllRoleOfUser;

/// <summary>
///     Get all role of user query model.
/// </summary>
public sealed class GetAllRoleOfUserQuery : IQuery<IList<string>>
{
    /// <summary>
    ///     User to check.
    /// </summary>
    public Domain.Entities.User User { get; set; }

    /// <summary>
    ///     How long the result should live in cache.
    /// </summary>
    /// <remarks>
    ///     Don't give this property a value when don't
    ///     want the result to be cached.
    /// </remarks>
    public int CacheExpiredTime { get; set; } = default;
}
