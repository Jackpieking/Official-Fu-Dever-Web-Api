using Application.Interfaces.Messaging;

namespace Application.Features.User.Queries.CheckUserPassword;

/// <summary>
///     Check user password query model.
/// </summary>
public sealed class CheckUserPasswordQuery : IQuery<bool>
{
    /// <summary>
    ///     User to check.
    /// </summary>
    public Domain.Entities.User User { get; set; }

    /// <summary>
    ///     Password of user account.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    ///     How long the result should live in cache.
    /// </summary>
    /// <remarks>
    ///     Don't give this property a value when don't
    ///     want the result to be cached.
    /// </remarks>
    public int CacheExpiredTime { get; set; } = default;
}
