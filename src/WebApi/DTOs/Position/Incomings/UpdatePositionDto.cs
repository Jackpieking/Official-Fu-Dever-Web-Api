using System.ComponentModel.DataAnnotations;
using WebApi.Attributes;
using WebApi.DTOs.Common;

namespace WebApi.DTOs.Position.Incomings;

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