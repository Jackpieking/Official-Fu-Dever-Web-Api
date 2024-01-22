using Application.Interfaces.Messaging;
using System.Collections.Generic;

namespace Application.Features.Skill.Queries.FindBySkillName;

/// <summary>
///     Find by skill name query modal.
/// </summary>
public sealed class FindBySkillNameQuery : IQuery<IEnumerable<Domain.Entities.Skill>>
{
    /// <summary>
    ///     Name of skill.
    /// </summary>
    public string SkillName { get; set; }
}
