using System.ComponentModel.DataAnnotations;
using WebApi.Attributes;
using WebApi.DTOs.Common;

namespace WebApi.DTOs.Skill.Incomings;

public sealed class UpdateSkillDto : IDtoNormalization
{
    [Required]
    [StringIsNotNullOrWhiteSpace]
    [MaxLength(
        length: Domain.Entities.Skill.Metadata.Name.MaxLength,
        ErrorMessage = "Too much chars on skill name !!")]
    [MinLength(
        length: Domain.Entities.Skill.Metadata.Name.MinLength,
        ErrorMessage = "Less than min length of skill name !!")]
    public string NewSkillName { get; set; }

    public void NormalizeAllProperties()
    {
        NewSkillName = NewSkillName.Trim();
    }
}