using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Messaging;
using Domain.UnitOfWorks;

namespace Application.Features.Skill.Commands.UpdateSkill;

/// <summary>
///     Update skill command handler.
/// </summary>
internal sealed class UpdateSkillCommandHandler : ICommandHandler<
    UpdateSkillCommand,
    bool>
{
    private readonly IUnitOfWork _unitOfWork;

    internal UpdateSkillCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    /// <summary>
    ///     Entry of new command.
    /// </summary>
    /// <param name="request">
    ///     Command request modal.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     A task containing the boolean result.
    /// </returns>
    public Task<bool> Handle(
        UpdateSkillCommand request,
        CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}
