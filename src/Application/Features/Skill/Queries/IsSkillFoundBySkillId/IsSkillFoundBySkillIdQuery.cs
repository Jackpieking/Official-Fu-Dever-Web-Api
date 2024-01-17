using System;
using Application.Interfaces.Messaging;

namespace Application.Features.Skill.Queries.IsSkillFoundBySkillId;

/// <summary>
///     Is skill found by skill id query modal.
/// </summary>
public sealed class IsSkillFoundBySkillIdQuery : IQuery<bool>
{
    /// <summary>
    ///     Skill Id.
    /// </summary>
    public Guid SkillId { get; set; }
}
