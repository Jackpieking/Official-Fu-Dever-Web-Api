using System;
using System.Collections.Generic;

namespace Application.Features.Skill.GetAllTemporarilyRemovedSkills;

/// <summary>
///     Get all temporarily removed skills response.
/// </summary>
public sealed class GetAllTemporarilyRemovedSkillsResponse
{
    public GetAllTemporarilyRemovedSkillsStatusCode StatusCode { get; init; }

    public IEnumerable<Skill> FoundTemporarilyRemovedSkills { get; init; }

    public sealed class Skill
    {
        public Guid SkillId { get; init; }

        public string SkillName { get; init; }

        public DateTime SkillRemovedAt { get; init; }

        public Guid SkillRemovedBy { get; init; }
    }
}
