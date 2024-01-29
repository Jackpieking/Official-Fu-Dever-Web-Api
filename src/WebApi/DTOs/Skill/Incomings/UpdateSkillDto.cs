using System.ComponentModel.DataAnnotations;
using WebApi.Attributes;
using WebApi.DTOs.Common;

namespace WebApi.DTOs.Skill.Incomings;

public sealed class UpdateSkillDto : IDtoNormalization
{
    [Required]
    [IsStringNotNull]
    public string NewName { get; set; }

    public void NormalizeAllProperties()
    {
        NewName = NewName.Trim();
    }
}