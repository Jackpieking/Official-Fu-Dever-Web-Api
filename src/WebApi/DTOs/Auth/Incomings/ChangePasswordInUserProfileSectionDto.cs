using System.ComponentModel.DataAnnotations;
using WebApi.Attributes;
using WebApi.DTOs.Common;

namespace WebApi.DTOs.Auth.Incomings;

public sealed class ChangePasswordInUserProfileSectionDto : IDtoNormalization
{
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
        CurrentPassword = CurrentPassword.Trim();
        NewPassword = NewPassword.Trim();
    }
}
