using System;

namespace WebApi.DTOs.Skill.Outgoings;

internal sealed class GetTemporarilyRemovedSkillDto
{
    internal Guid Id { get; set; }

    internal string Name { get; set; }

    internal DateTime RemovedAt { get; set; }

    internal Guid RemovedBy { get; set; }
}