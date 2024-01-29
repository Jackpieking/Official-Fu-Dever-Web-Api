using System.ComponentModel.DataAnnotations;
using WebApi.Attributes;
using WebApi.DTOs.Common;

namespace WebApi.DTOs.Auth.Incomings;

public sealed class RegisterAsUserDto : IDtoNormalization
{
    [Required]
    [EmailAddress]
    [IsStringNotNull]
    public string Username { get; set; }

    [Required]
    [MinLength(length: 3)]
    [DataType(dataType: DataType.Password)]
    [IsStringNotNull]
    public string Password { get; set; }

    public void NormalizeAllProperties()
    {
        Username = Username.Trim();
        Password = Password.Trim();
    }
}