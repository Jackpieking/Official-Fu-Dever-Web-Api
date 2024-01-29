using System.ComponentModel.DataAnnotations;
using WebApi.Attributes;
using WebApi.DTOs.Common;

namespace WebApi.DTOs.Auth.Incomings;

public sealed class RefreshAccessTokenDto : IDtoNormalization
{
    [Required]
    [IsStringNotNull]
    public string RefreshToken { get; set; }

    public void NormalizeAllProperties()
    {
        RefreshToken = RefreshToken.Trim();
    }
}