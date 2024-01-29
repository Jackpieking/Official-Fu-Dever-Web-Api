using Application.Interfaces.Messaging;

namespace Application.Features.Skill.Commands.CreateSkill;

/// <summary>
///     Represent create skill command model.
/// </summary>
public sealed class CreateSkillCommand : ICommand<bool>
{
    /// <summary>
    ///     Name of new skill.
    /// </summary>
    public string NewSkillName { get; set; }
}
