using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Messaging;
using Domain.UnitOfWorks;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Skill.Commands.RemoveSkillTemporarily;

/// <summary>
///     Remove skill temporarily command handler.
/// </summary>
internal sealed class RemoveSkillTemporarilyCommandHandler : ICommandHandler<
    RemoveSkillTemporarilyCommand,
    bool>
{
    private readonly IUnitOfWork _unitOfWork;

    internal RemoveSkillTemporarilyCommandHandler(IUnitOfWork unitOfWork)
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
    public async Task<bool> Handle(
        RemoveSkillTemporarilyCommand request,
        CancellationToken cancellationToken)
    {
        if (request.SkillId == Guid.Empty ||
            request.SkillRemovedBy == Guid.Empty)
        {
            return false;
        }

        var executedTransactionResult = false;

        await _unitOfWork
            .CreateExecutionStrategy()
            .ExecuteAsync(operation: async () =>
            {
                await _unitOfWork.CreateTransactionAsync(cancellationToken: cancellationToken);

                try
                {
                    await _unitOfWork.SkillRepository.BulkUpdateBySkillIdAsync(
                        skillId: request.SkillId,
                        skillRemovedAt: request.SkillRemovedAt,
                        skillRemovedBy: request.SkillRemovedBy,
                        cancellationToken: cancellationToken);

                    await _unitOfWork.CommitTransactionAsync(cancellationToken: cancellationToken);

                    executedTransactionResult = true;
                }
                catch
                {
                    await _unitOfWork.RollBackTransactionAsync(cancellationToken: cancellationToken);
                }
                finally
                {
                    await _unitOfWork.DisposeTransactionAsync();
                }
            });

        return executedTransactionResult;
    }
}
