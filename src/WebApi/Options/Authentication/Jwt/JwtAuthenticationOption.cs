namespace WebApi.Options.Authentication.Jwt;

public sealed class JwtAuthenticationOption
{
    public CommonOption Common { get; set; } = new();

    public TypeOption Type { get; set; } = new();

    public sealed class CommonOption
    {
        public string DefaultAuthenticateScheme { get; set; }

        public string DefaultScheme { get; set; }

        public string DefaultChallengeScheme { get; set; }
    }

    public sealed class TypeOption
    {
        public JwtOption Jwt { get; set; } = new();

        public sealed class JwtOption
        {
            public bool ValidateIssuer { get; set; }

            public bool ValidateAudience { get; set; }

            public bool ValidateLifetime { get; set; }

            public bool ValidateIssuerSigningKey { get; set; }

            public bool RequireExpirationTime { get; set; }

            public string ValidIssuer { get; set; }

            public string ValidAudience { get; set; }

            public string IssuerSigningKey { get; set; }
        }
    }
}

