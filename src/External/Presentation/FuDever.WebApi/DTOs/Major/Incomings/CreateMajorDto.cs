using FuDever.WebApi.Attributes;
using FuDever.WebApi.DTOs.Common;
using System.ComponentModel.DataAnnotations;

namespace FuDever.WebApi.DTOs.Major.Incomings;

/// <summary>
///     Incoming DTO for creating new Major.
/// </summary>
public sealed class CreateMajorDto : IDtoNormalization
{
    [Required]
    [StringIsNotNullOrWhiteSpace]
    [MaxLength(
        length: Domain.Entities.Major.Metadata.Name.MaxLength,
        ErrorMessage = "Too much chars on major name !!")]
    [MinLength(
        length: Domain.Entities.Major.Metadata.Name.MinLength,
        ErrorMessage = "Less than min length of major name !!")]
    public string MajorName { get; set; }

    public void NormalizeAllProperties()
    {
        MajorName = MajorName.Trim();
    }
}
