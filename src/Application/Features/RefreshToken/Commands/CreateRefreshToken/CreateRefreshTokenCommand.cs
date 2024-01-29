using System.Collections.Generic;
using System.Security.Claims;
using Application.Interfaces.Messaging;

namespace Application.Features.RefreshToken.Commands.CreateRefreshToken;

/// <summary>
///     Create refresh token command model.
/// </summary>
public sealed class CreateRefreshTokenCommand : ICommand<string>
{
    /// <summary>
    ///     List of user claims.
    /// </summary>
    public IEnumerable<Claim> UserClaims { get; set; }

    /// <summary>
    ///     Does user want the system to remember.
    /// </summary>
    public bool RememberMe { get; set; }

    /// <summary>
    ///     How long is the length of refresh token value.
    /// </summary>
    public int ValueLength { get; set; }
}
