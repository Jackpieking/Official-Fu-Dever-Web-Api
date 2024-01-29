using Application.Interfaces.Messaging;
using System;

namespace Application.Features.Skill.Commands.UpdateSkill;

/// <summary>
///     Update skill command model.
/// </summary>
public sealed class UpdateSkillCommand : ICommand<bool>
{
    /// <summary>
    ///     Id of updated skill.
    /// </summary>
    public Guid SkillId { get; set; }

    /// <summary>
    ///     Name of updated skill.
    /// </summary>
    public string NewSkillName { get; set; }

    /// <summary>
    ///     Skill is updated by whom.
    /// </summary>
    public Guid SkillUpdatedBy { get; set; }
}
