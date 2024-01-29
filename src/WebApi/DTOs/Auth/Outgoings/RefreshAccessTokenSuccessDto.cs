namespace WebApi.DTOs.Auth.Outgoings;

public sealed class RefreshAccessTokenSuccessDto
{
    public string AccessToken { get; set; }

    public string RefreshToken { get; set; }
}