namespace WebApi.Options.Authentication.Jwt;

internal sealed class JwtAuthenticationOption
{
    internal CommonOption Common { get; set; } = new();

    internal TypeOption Type { get; set; } = new();

    internal sealed class CommonOption
    {
        internal string DefaultAuthenticateScheme { get; set; }

        internal string DefaultScheme { get; set; }

        internal string DefaultChallengeScheme { get; set; }
    }

    internal sealed class TypeOption
    {
        internal JwtOption Jwt { get; set; } = new();

        internal sealed class JwtOption
        {
            internal bool ValidateIssuer { get; set; }

            internal bool ValidateAudience { get; set; }

            internal bool ValidateLifetime { get; set; }

            internal bool ValidateIssuerSigningKey { get; set; }

            internal bool RequireExpirationTime { get; set; }

            internal string ValidIssuer { get; set; }

            internal string ValidAudience { get; set; }

            internal string IssuerSigningKey { get; set; }
        }
    }
}

