using System;
using Application.Interfaces.Messaging;

namespace Application.Features.Skill.Commands.RemoveSkillTemporarily;

/// <summary>
///     Remove skill temporarily command modal.
/// </summary>
public sealed class RemoveSkillTemporarilyCommand : ICommand<bool>
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
