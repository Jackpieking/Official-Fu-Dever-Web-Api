using System.ComponentModel.DataAnnotations;
using WebApi.Attributes;
using WebApi.DTOs.Common;

namespace WebApi.DTOs.Skill.Incomings;

public sealed class CreateSkillDto : IDtoNormalization
{
    [Required]
    [IsStringNotNull]
    public string SkillName { get; set; }

    public void NormalizeAllProperties()
    {
        SkillName = SkillName.Trim();
    }
}