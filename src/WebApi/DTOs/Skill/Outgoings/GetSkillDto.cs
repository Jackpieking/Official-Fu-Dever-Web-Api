using System;

namespace WebApi.DTOs.Skill.Outgoings;

internal sealed class GetSkillDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}