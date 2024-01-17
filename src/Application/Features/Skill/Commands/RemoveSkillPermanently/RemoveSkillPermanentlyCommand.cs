using System;
using Application.Interfaces.Messaging;

namespace Application.Features.Skill.Commands.RemoveSkillPermanently;

/// <summary>
///     Represent remove skill permanently command modal.
/// </summary>
public sealed class RemoveSkillPermanentlyCommand : ICommand<bool>
{
    /// <summary>
    ///     Id of removed skill.
    /// </summary>
    public Guid SkillId { get; set; }

    /// <summary>
    ///     Skill is removed by whom.
    /// </summary>
    public Guid SkillRemovedBy { get; set; }
}
