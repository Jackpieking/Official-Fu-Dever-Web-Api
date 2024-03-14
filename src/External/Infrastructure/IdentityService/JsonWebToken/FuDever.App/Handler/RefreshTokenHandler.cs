using FuDever.Application.Interfaces.Authentication.Jwt;
using FuDever.Domain.Entities;
using System.Linq;
using System.Security.Cryptography;

namespace FuDever.App.Handler;

/// <summary>
///     Implementation refresh token generator interface.
/// </summary>
internal sealed class RefreshTokenHandler : IRefreshTokenHandler
{
    public string Generate(int length)
    {
        if (length < RefreshToken.Metadata.Value.MinLength ||
            length > RefreshToken.Metadata.Value.MaxLength)
        {
            return string.Empty;
        }

        const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890abcdefghijklmnopqrstuvwxyz!@#$%^&*+=";

        return new(
            value: Enumerable
                .Repeat(element: Chars, count: length)
                .Select(selector: s => s[index:
                    RandomNumberGenerator.GetInt32(toExclusive: s.Length)])
                .ToArray()
        );
    }
}
