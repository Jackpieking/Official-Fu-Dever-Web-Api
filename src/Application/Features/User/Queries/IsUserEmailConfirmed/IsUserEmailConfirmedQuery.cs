using Application.Interfaces.Messaging;

namespace Application.Features.User.Queries.IsUserEmailConfirmed;

/// <summary>
///     Is user email confirmed query model.
/// </summary>
public sealed class IsUserEmailConfirmedQuery : IQuery<bool>
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
