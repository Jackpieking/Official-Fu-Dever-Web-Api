using System.Collections.Generic;
using Application.Interfaces.Messaging;

namespace Application.Features.Skill.Queries.GetAllSkill;

/// <summary>
///     Get all skill query modal.
/// </summary>
public sealed class GetAllSkillQuery : IQuery<IEnumerable<Domain.Entities.Skill>>
{

}
