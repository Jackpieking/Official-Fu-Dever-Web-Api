using Application.Interfaces.Authentication.Jwt;
using Domain.Entities;
using System;
using System.Linq;

namespace Generator.Handler.Token;

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

        Random random = new();

        return new(
            value: Enumerable
                .Repeat(element: Chars, count: length)
                .Select(selector: s => s[random.Next(maxValue: s.Length)])
                .ToArray()
        );
    }
}
