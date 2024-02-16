using System.ComponentModel.DataAnnotations;
using WebApi.Attributes;
using WebApi.DTOs.Common;

namespace FuDeverWebApi.Presentation.DTOs.Auth.Incomings;

public sealed class ChangePasswordInForgotPasswordSectionDto : IDtoNormalization
{
    [Required]
    [EmailAddress]
    [StringIsNotNullOrWhiteSpace]
    public string Username { get; set; }

    [Required]
    [MinLength(length: 3)]
    [DataType(dataType: DataType.Password)]
    [StringIsNotNullOrWhiteSpace]
    public string CurrentPassword { get; set; }

    [Required]
    [MinLength(length: 3)]
    [DataType(dataType: DataType.Password)]
    [StringIsNotNullOrWhiteSpace]
    public string NewPassword { get; set; }

    public void NormalizeAllProperties()
    {
        Username = Username.Trim();
        CurrentPassword = CurrentPassword.Trim();
        NewPassword = NewPassword.Trim();
    }
}