using FuDever.WebApi.Attributes;
using FuDever.WebApi.DTOs.Common;
using System.ComponentModel.DataAnnotations;

namespace FuDever.WebApi.DTOs.Role.Incomings;

/// <summary>
///     Incoming DTO for updating existing Role.
/// </summary>
public sealed class UpdateRoleDto : IDtoNormalization
{
    [Required]
    [StringIsNotNullOrWhiteSpace]
    [MaxLength(
        length: Domain.Entities.Role.Metadata.Name.MaxLength,
        ErrorMessage = "Too much chars on role name !!")]
    [MinLength(
        length: Domain.Entities.Role.Metadata.Name.MinLength,
        ErrorMessage = "Less than min length of role name !!")]
    public string NewRoleName { get; set; }

    public void NormalizeAllProperties()
    {
        NewRoleName = NewRoleName.Trim();
    }
}