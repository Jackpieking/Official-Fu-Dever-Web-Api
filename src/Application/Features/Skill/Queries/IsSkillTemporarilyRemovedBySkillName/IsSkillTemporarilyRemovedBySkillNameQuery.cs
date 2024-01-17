using Application.Interfaces.Messaging;

namespace Application.Features.Skill.Queries.IsSkillTemporarilyRemovedBySkillName;

/// <summary>
///     Is skill temporarily removed by skill name query modal.
/// </summary>
public sealed class IsSkillTemporarilyRemovedBySkillNameQuery : IQuery<bool>
{
    /// <summary>
    ///     Name of skill.
    /// </summary>
    public string SkillName { get; set; }
}
