using System.ComponentModel.DataAnnotations;
using WebApi.Attributes;
using WebApi.DTOs.Common;

namespace WebApi.DTOs.Auth.Incomings;

public sealed class RegisterAsAdminDto : IDtoNormalization
{
    [Required]
    [EmailAddress]
    [StringIsNotNullOrWhiteSpace]
    public string Username { get; set; }

    [Required]
    [MinLength(length: 3)]
    [DataType(dataType: DataType.Password)]
    [StringIsNotNullOrWhiteSpace]
    public string Password { get; set; }

    [Required]
    [StringIsNotNullOrWhiteSpace]
    public string AdminConfirmedKey { get; set; }

    public void NormalizeAllProperties()
    {
        Username = Username.Trim();
        Password = Password.Trim();
        AdminConfirmedKey = AdminConfirmedKey.Trim();
    }
}
