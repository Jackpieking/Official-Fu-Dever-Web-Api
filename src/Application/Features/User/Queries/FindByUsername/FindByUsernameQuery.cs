using Application.Interfaces.Messaging;

namespace Application.Features.User.Queries.FindByUsername;

/// <summary>
///     Find by username query model.
/// </summary>
public sealed class FindByUsernameQuery : IQuery<Domain.Entities.User>
{
    /// <summary>
    ///     Username of user account.
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    ///     How long the result should live in cache.
    /// </summary>
    /// <remarks>
    ///     Don't give this property a value when don't
    ///     want the result to be cached.
    /// </remarks>
    public int CacheExpiredTime { get; set; } = default;
}
