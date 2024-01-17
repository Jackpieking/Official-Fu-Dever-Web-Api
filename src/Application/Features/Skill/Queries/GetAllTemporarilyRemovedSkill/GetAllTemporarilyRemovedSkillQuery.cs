using System.Collections.Generic;
using Application.Interfaces.Messaging;

namespace Application.Features.Skill.Queries.GetAllTemporarilyRemovedSkill;

/// <summary>
///     Get all temporarily removed skill query modal.
/// </summary>
public sealed class GetAllTemporarilyRemovedSkillQuery : IQuery<IEnumerable<Domain.Entities.Skill>>
{

}
