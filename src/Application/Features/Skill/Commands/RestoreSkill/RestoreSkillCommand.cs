using Application.Interfaces.Messaging;
using System;

namespace Application.Features.Skill.Commands.RestoreSkill;

/// <summary>
///     Restore skill command model.
/// </summary>
public sealed class RestoreSkillCommand : ICommand<bool>
{
    /// <summary>
    ///     Id of restored skill.
    /// </summary>
    public Guid SkillId { get; set; }
}
