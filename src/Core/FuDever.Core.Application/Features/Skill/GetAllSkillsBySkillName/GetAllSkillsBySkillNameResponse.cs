using System;
using System.Collections.Generic;

namespace FuDever.Application.Features.Skill.GetAllSkillsBySkillName;

/// <summary>
///     Get all skills by name response.
/// </summary>
public sealed class GetAllSkillsBySkillNameResponse
{
    public GetAllSkillsBySkillNameResponseStatusCode StatusCode { get; init; }

    public IEnumerable<Skill> FoundSkills { get; init; }

    public sealed class Skill
    {
        public Guid SkillId { get; init; }

        public string SkillName { get; init; }
    }
}
