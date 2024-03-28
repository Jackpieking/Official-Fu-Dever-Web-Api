using FuDever.Application.Features.Auth.Login.Middlewares;
using MediatR;

namespace FuDever.Application.Features.Auth.Login;

/// <summary>
///     Request of login feature.
/// </summary>
public sealed class LoginRequest :
    IRequest<LoginResponse>,
    ILoginMiddleware
{
    /// <summary>
    ///     How long the result should live in cache.
    /// </summary>
    /// <remarks>
    ///     Don't give this property a value when don't
    ///     want the result to be cached.
    /// </remarks>
    public int CacheExpiredTime { get; init; }

    public string Username { get; init; }

    public string Password { get; init; }

    public bool RememberMe { get; init; }
}
