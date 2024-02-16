using System.Collections.Generic;
using System.Security.Claims;

namespace Application.Interfaces.Authentication.Jwt;

/// <summary>
///     Represent jwt generator interface.
/// </summary>
public interface IAccessTokenHandler
{
    /// <summary>
    ///     Generate jwt base on list of claims.
    /// </summary>
    /// <param name="claims">
    ///     List of user claims.
    /// </param>
    /// <returns>
    ///     A string having format of jwt
    ///     or empty string if validate fail.
    /// </returns>
    string Generate(IEnumerable<Claim> claims);

    /// <summary>
    ///     Validate an access token.
    /// </summary>
    /// <param name="token">
    ///     Token in jwt format.
    /// </param>
    /// <returns>
    ///     True if satisfy all conditions.
    ///     Otherwise, false.
    /// </returns>
    bool Validate(string token);
}
