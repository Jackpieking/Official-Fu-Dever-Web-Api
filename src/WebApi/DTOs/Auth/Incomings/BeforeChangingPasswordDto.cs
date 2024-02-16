using System.ComponentModel.DataAnnotations;
using WebApi.Attributes;
using WebApi.DTOs.Common;

namespace WebApi.DTOs.Auth.Incomings;

public sealed class BeforeChangingPasswordDto : IDtoNormalization
{
    [Required]
    [EmailAddress]
    [StringIsNotNullOrWhiteSpace]
    public string Username { get; set; }

    public void NormalizeAllProperties()
    {
        Username = Username.Trim();
    }
}
