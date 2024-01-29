using Application.Interfaces.Messaging;
using System.Collections.Generic;

namespace Application.Features.Skill.Queries.FindBySkillName;

/// <summary>
///     Find by skill name query model.
/// </summary>
public sealed class FindBySkillNameQuery : IQuery<IEnumerable<Domain.Entities.Skill>>
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
