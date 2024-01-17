using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Messaging;
using Domain.UnitOfWorks;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Skill.Commands.RestoreSkill;

/// <summary>
///     Restore skill command handler.
/// </summary>
internal sealed class RestoreSkillCommandHandler : ICommandHandler<
    RestoreSkillCommand,
    bool>
{
    private readonly IUnitOfWork _unitOfWork;

    internal RestoreSkillCommandHandler(IUnitOfWork unitOfWork)
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
        RestoreSkillCommand request,
        CancellationToken cancellationToken)
    {
        if (request.SkillId == Guid.Empty ||
            request.SkillRestoredBy == Guid.Empty)
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
                        skillRemovedAt: request.SkillRestoredAt,
                        skillRemovedBy: request.SkillRestoredBy,
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
