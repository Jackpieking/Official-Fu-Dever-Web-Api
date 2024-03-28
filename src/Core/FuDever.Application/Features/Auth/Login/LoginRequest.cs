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
    public string Username { get; init; }

    public string Password { get; init; }

    public bool RememberMe { get; init; }
}
