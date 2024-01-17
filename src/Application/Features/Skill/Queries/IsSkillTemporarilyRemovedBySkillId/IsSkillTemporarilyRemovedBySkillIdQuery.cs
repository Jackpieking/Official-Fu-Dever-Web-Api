using System;
using Application.Interfaces.Messaging;

namespace Application.Features.Skill.Queries.IsSkillTemporarilyRemovedBySkillId;

/// <summary>
///     Is skill temporarily removed by skill id query modal.
/// </summary>
public sealed class IsSkillTemporarilyRemovedBySkillIdQuery : IQuery<bool>
{
    /// <summary>
    ///     Skill Id.
    /// </summary>
    public Guid SkillId { get; set; }
}
