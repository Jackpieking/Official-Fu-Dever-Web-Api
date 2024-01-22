using Application.Interfaces.Data;
using Application.Interfaces.Messaging;
using Domain.UnitOfWorks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Skill.Commands.RestoreSkill;

/// <summary>
///     Restore skill command handler.
/// </summary>
internal sealed class RestoreSkillCommandHandler : ICommandHandler<
    RestoreSkillCommand,
    bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDbMinTimeHandler _dbMinTimeHandler;
    private readonly IValidator<RestoreSkillCommand> _validator;

    public RestoreSkillCommandHandler(
        IUnitOfWork unitOfWork,
        IDbMinTimeHandler dbMinTimeHandler,
        IValidator<RestoreSkillCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _dbMinTimeHandler = dbMinTimeHandler;
        _validator = validator;
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
        var inputValidationResult = await _validator.ValidateAsync(
            instance: request,
            cancellation: cancellationToken);

        if (!inputValidationResult.IsValid)
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
                    await _unitOfWork.SkillRepository.BulkUpdateBySkillIdVer1Async(
                        skillId: request.SkillId,
                        skillRemovedAt: _dbMinTimeHandler.Get(),
                        skillRemovedBy: Guid.Empty,
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
