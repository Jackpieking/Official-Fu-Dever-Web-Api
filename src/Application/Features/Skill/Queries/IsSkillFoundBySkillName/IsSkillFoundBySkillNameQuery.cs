using Application.Interfaces.Messaging;

namespace Application.Features.Skill.Queries.IsSkillFoundBySkillName;

/// <summary>
///     Is skill found by skill name query modal.
/// </summary>
public sealed class IsSkillFoundBySkillNameQuery : IQuery<bool>
{
    /// <summary>
    ///     Name of skill.
    /// </summary>
    public string SkillName { get; set; }
}
