namespace WebApi.DTOs.Auth.Outgoings;

internal sealed class LoginUserSuccessDto
{
    public string AccessToken { get; set; }

    public string RefreshToken { get; set; }

    public UserCredentialDto UserCredential { get; set; }

    public class UserCredentialDto
    {
        public string Email { get; set; }

        public string AvatarUrl { get; set; }
    }
}