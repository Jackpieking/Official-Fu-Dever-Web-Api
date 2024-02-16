using System;
using System.Collections.Generic;

namespace Application.Features.Skill.GetAllSkills;

/// <summary>
///     Get all skills response.
/// </summary>
public sealed class GetAllSkillsResponse
{
    public GetAllSkillsStatusCode StatusCode { get; init; }

    public IEnumerable<Skill> FoundSkills { get; init; }

    public sealed class Skill
    {
        public Guid SkillId { get; init; }

        public string SkillName { get; init; }
    }
}
