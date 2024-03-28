namespace FuDever.Application.Features.Auth.Login;

/// <summary>
///     Login response.
/// </summary>
public sealed class LoginResponse
{
    public LoginResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; set; }

    public sealed class Body
    {
        public string AccessToken { get; init; }

        public string RefreshToken { get; init; }

        public UserCredential User { get; init; }

        public sealed class UserCredential
        {
            public string Email { get; init; }

            public string AvatarUrl { get; init; }
        }
    }
}
