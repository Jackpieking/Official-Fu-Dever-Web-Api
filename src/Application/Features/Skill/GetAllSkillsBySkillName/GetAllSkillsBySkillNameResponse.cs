using System;
using System.Collections.Generic;
using Application.Features.Skill.GetAllSkillsByName;

namespace Application.Features.Skill.GetAllSkillsBySkillName;

/// <summary>
///     Get all skills by name response.
/// </summary>
public sealed class GetAllSkillsBySkillNameResponse
{
    public GetAllSkillsBySkillNameStatusCode StatusCode { get; init; }

    public IEnumerable<Skill> FoundSkills { get; init; }

    public sealed class Skill
    {
        public Guid SkillId { get; init; }

        public string SkillName { get; init; }
    }
}
