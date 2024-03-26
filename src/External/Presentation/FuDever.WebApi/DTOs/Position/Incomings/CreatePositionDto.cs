using FuDever.WebApi.Attributes;
using FuDever.WebApi.DTOs.Common;
using System.ComponentModel.DataAnnotations;

namespace FuDever.WebApi.DTOs.Position.Incomings;

/// <summary>
///     Incoming DTO for creating new Position.
/// </summary>
public sealed class CreatePositionDto : IDtoNormalization
{
    [Required]
    [StringIsNotNullOrWhiteSpace]
    [MaxLength(
        length: Domain.Entities.Position.Metadata.Name.MaxLength,
        ErrorMessage = "Too much chars on position name !!")]
    [MinLength(
        length: Domain.Entities.Position.Metadata.Name.MinLength,
        ErrorMessage = "Less than min length of position name !!")]
    public string PositionName { get; set; }

    public void NormalizeAllProperties()
    {
        PositionName = PositionName.Trim();
    }
}