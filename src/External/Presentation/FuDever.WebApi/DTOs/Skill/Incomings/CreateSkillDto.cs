using FuDever.WebApi.Attributes;
using FuDever.WebApi.DTOs.Common;
using System.ComponentModel.DataAnnotations;

namespace FuDever.WebApi.DTOs.Skill.Incomings;

public sealed class CreateSkillDto : IDtoNormalization
{
    [Required]
    [StringIsNotNullOrWhiteSpace]
    [MaxLength(
        length: Domain.Entities.Skill.Metadata.Name.MaxLength,
        ErrorMessage = "Too much chars on skill name !!")]
    [MinLength(
        length: Domain.Entities.Skill.Metadata.Name.MinLength,
        ErrorMessage = "Less than min length of skill name !!")]
    public string SkillName { get; set; }

    public void NormalizeAllProperties()
    {
        SkillName = SkillName.Trim();
    }
}