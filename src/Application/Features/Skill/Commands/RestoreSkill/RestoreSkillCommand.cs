using System;
using Application.Interfaces.Messaging;

namespace Application.Features.Skill.Commands.RestoreSkill;

/// <summary>
///     Restore skill command modal.
/// </summary>
public sealed class RestoreSkillCommand : ICommand<bool>
{
    /// <summary>
    ///     Id of restored skill.
    /// </summary>
    public Guid SkillId { get; set; }

    /// <summary>
    ///     Skill is restore by whom.
    /// </summary>
    public Guid SkillRestoredBy { get; set; }

    /// <summary>
    ///     When is skill restored.
    /// </summary>
    public DateTime SkillRestoredAt { get; set; }
}
