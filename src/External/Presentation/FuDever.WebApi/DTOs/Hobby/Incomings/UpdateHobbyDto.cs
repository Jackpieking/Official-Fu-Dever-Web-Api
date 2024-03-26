using FuDever.WebApi.Attributes;
using FuDever.WebApi.DTOs.Common;
using System.ComponentModel.DataAnnotations;

namespace FuDever.WebApi.DTOs.Hobby.Incomings;

/// <summary>
///     Incoming DTO for updating existing hobby.
/// </summary>
public sealed class UpdateHobbyDto : IDtoNormalization
{
    [Required]
    [StringIsNotNullOrWhiteSpace]
    [MaxLength(
        length: Domain.Entities.Hobby.Metadata.Name.MaxLength,
        ErrorMessage = "Too much chars on hobby name !!")]
    [MinLength(
        length: Domain.Entities.Hobby.Metadata.Name.MinLength,
        ErrorMessage = "Less than min length of hobby name !!")]
    public string NewHobbyName { get; set; }

    public void NormalizeAllProperties()
    {
        NewHobbyName = NewHobbyName.Trim();
    }
}
