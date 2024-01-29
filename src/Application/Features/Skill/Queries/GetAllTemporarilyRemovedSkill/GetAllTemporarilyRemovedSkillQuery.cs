using Application.Interfaces.Messaging;
using System.Collections.Generic;

namespace Application.Features.Skill.Queries.GetAllTemporarilyRemovedSkill;

/// <summary>
///     Get all temporarily removed skill query model.
/// </summary>
public sealed class GetAllTemporarilyRemovedSkillQuery : IQuery<IEnumerable<Domain.Entities.Skill>>
{
    /// <summary>
    ///     How long the result should live in cache.
    /// </summary>
    /// <remarks>
    ///     Don't give this property a value when don't
    ///     want the result to be cached.
    /// </remarks>
    public int CacheExpiredTime { get; set; } = default;
}
