namespace WebApi.DTOs.Auth.Outgoings;

internal sealed class RefreshAccessTokenSuccessDto
{
    public string AccessToken { get; set; }

    public string RefreshToken { get; set; }
}