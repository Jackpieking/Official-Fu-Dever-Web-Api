using Application.Interfaces.Authentication.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace AuthenticationHandler.Handler.Jwt;

/// <summary>
///     Implementation of jwt generator interface.
/// </summary>
internal sealed class AccessTokenHandler : IAccessTokenHandler
{
    private readonly SecurityTokenHandler _securityTokenHandler;
    private readonly TokenValidationParameters _tokenValidationParameters;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AccessTokenHandler(
        SecurityTokenHandler securityTokenHandler,
        TokenValidationParameters tokenValidationParameters,
        IHttpContextAccessor httpContextAccessor)
    {
        _securityTokenHandler = securityTokenHandler;
        _tokenValidationParameters = tokenValidationParameters;
        _httpContextAccessor = httpContextAccessor;
    }

    public string Generate(IEnumerable<Claim> claims)
    {
        // Validate set of user claims.
        if (claims.Equals(Enumerable.Empty<Claim>()) ||
            Equals(objA: claims, objB: null))
        {
            return string.Empty;
        }

        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Audience = _tokenValidationParameters.ValidAudience,
            Issuer = _tokenValidationParameters.ValidIssuer,
            Subject = new(claims: claims),
            Expires = DateTime.UtcNow.AddHours(value: 1),
            SigningCredentials = new(
                key: _tokenValidationParameters.IssuerSigningKey,
                algorithm: SecurityAlgorithms.HmacSha256)
        };

        var token = _securityTokenHandler.CreateToken(tokenDescriptor: tokenDescriptor);

        return _securityTokenHandler.WriteToken(token: token);
    }

    public bool Validate(string token)
    {
        // Validate access token.
        try
        {
            _securityTokenHandler.ValidateToken(
                securityToken: token,
                validationParameters: _tokenValidationParameters,
                out _);
        }
        catch
        {
            return default;
        }

        var userClaimPrinciple = _httpContextAccessor.HttpContext.User;

        // Does token contain these claims.
        if (!userClaimPrinciple.HasClaim(match: claim =>
                claim.Type.Equals(value: JwtRegisteredClaimNames.Jti)) ||
            !userClaimPrinciple.HasClaim(match: claim =>
                claim.Type.Equals(value: JwtRegisteredClaimNames.Sub)) ||
            !userClaimPrinciple.HasClaim(match: claim =>
                claim.Type.Equals(value: "role")))
        {
            return default;
        }

        // Validate the format of Jti and Sub claim.
        if (!Guid.TryParse(
                input: userClaimPrinciple.FindFirstValue(
                    claimType: JwtRegisteredClaimNames.Jti),
                result: out _) ||
            !Guid.TryParse(
                input: userClaimPrinciple.FindFirstValue(
                    claimType: JwtRegisteredClaimNames.Sub),
                result: out _))
        {
            return default;
        }

        return true;
    }
}
