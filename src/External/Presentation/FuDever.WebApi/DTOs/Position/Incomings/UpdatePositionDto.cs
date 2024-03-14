using FuDever.WebApi.Attributes;
using FuDever.WebApi.DTOs.Common;
using System.ComponentModel.DataAnnotations;

namespace FuDever.WebApi.DTOs.Position.Incomings;

public sealed class UpdatePositionDto : IDtoNormalization
{
    [Required]
    [StringIsNotNullOrWhiteSpace]
    [MaxLength(
        length: Domain.Entities.Position.Metadata.Name.MaxLength,
        ErrorMessage = "Too much chars on position name !!")]
    [MinLength(
        length: Domain.Entities.Position.Metadata.Name.MinLength,
        ErrorMessage = "Less than min length of position name !!")]
    public string NewPositionName { get; set; }

    public void NormalizeAllProperties()
    {
        NewPositionName = NewPositionName.Trim();
    }
}