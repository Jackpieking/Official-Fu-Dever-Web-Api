using Application.Interfaces.Messaging;
using System;

namespace Application.Features.Skill.Queries.IsSkillFoundBySkillId;

/// <summary>
///     Is skill found by skill id query model.
/// </summary>
public sealed class IsSkillFoundBySkillIdQuery : IQuery<bool>
{
    /// <summary>
    ///     Skill Id.
    /// </summary>
    public Guid SkillId { get; set; }

    /// <summary>
    ///     How long the result should live in cache.
    /// </summary>
    /// <remarks>
    ///     Don't give this property a value when don't
    ///     want the result to be cached.
    /// </remarks>
    public int CacheExpiredTime { get; set; } = default;
}
