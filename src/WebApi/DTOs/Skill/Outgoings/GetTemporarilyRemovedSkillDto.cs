using System;

namespace WebApi.DTOs.Skill.Outgoings;

internal sealed class GetTemporarilyRemovedSkillDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }
}