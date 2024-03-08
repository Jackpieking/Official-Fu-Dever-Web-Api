using System.ComponentModel.DataAnnotations;
using WebApi.Attributes;
using WebApi.DTOs.Common;

namespace WebApi.DTOs.Position.Incomings;

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