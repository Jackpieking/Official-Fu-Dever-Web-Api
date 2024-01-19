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
}
