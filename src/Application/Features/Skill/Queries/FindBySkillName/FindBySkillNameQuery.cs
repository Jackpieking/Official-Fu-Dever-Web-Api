using System.Collections.Generic;
using Application.Interfaces.Messaging;

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
