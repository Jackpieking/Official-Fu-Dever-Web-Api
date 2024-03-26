using FuDever.WebApi.Attributes;
using FuDever.WebApi.DTOs.Common;
using System.ComponentModel.DataAnnotations;

namespace FuDever.WebApi.DTOs.Hobby.Incomings;

/// <summary>
///     Incoming DTO for creating new hobby.
/// </summary>
public sealed class CreateHobbyDto : IDtoNormalization
{
    [Required]
    [StringIsNotNullOrWhiteSpace]
    [MaxLength(
        length: Domain.Entities.Hobby.Metadata.Name.MaxLength,
        ErrorMessage = "Too much chars on hobby name !!")]
    [MinLength(
        length: Domain.Entities.Hobby.Metadata.Name.MinLength,
        ErrorMessage = "Less than min length of hobby name !!")]
    public string HobbyName { get; set; }

    public void NormalizeAllProperties()
    {
        HobbyName = HobbyName.Trim();
    }
}
