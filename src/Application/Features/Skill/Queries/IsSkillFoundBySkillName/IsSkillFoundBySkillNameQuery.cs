using Application.Interfaces.Messaging;

namespace Application.Features.Skill.Queries.IsSkillFoundBySkillName;

/// <summary>
///     Is skill found by skill name query model.
/// </summary>
public sealed class IsSkillFoundBySkillNameQuery : IQuery<bool>
{
    /// <summary>
    ///     Name of skill.
    /// </summary>
    public string SkillName { get; set; }

    /// <summary>
    ///     How long the result should live in cache.
    /// </summary>
    /// <remarks>
    ///     Don't give this property a value when don't
    ///     want the result to be cached.
    /// </remarks>
    public int CacheExpiredTime { get; set; } = default;
}
