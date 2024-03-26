using FuDever.WebApi.Attributes;
using FuDever.WebApi.DTOs.Common;
using System.ComponentModel.DataAnnotations;

namespace FuDever.WebApi.DTOs.Platform.Incomings;

/// <summary>
///     Incoming DTO for updating existing Platform.
/// </summary>
public sealed class UpdatePlatformDto : IDtoNormalization
{
    [Required]
    [StringIsNotNullOrWhiteSpace]
    [MaxLength(
        length: Domain.Entities.Platform.Metadata.Name.MaxLength,
        ErrorMessage = "Too much chars on platform name !!")]
    [MinLength(
        length: Domain.Entities.Platform.Metadata.Name.MinLength,
        ErrorMessage = "Less than min length of platform name !!")]
    public string NewPlatformName { get; set; }

    public void NormalizeAllProperties()
    {
        NewPlatformName = NewPlatformName.Trim();
    }
}
