using Application.Commons;
using Application.Interfaces.Data;
using Application.Interfaces.Messaging;
using Domain.UnitOfWorks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Skill.Commands.CreateSkill;

/// <summary>
///     Represent create skill command handler.
/// </summary>
internal sealed class CreateSkillCommandHandler : ICommandHandler<CreateSkillCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDbMinTimeHandler _dbMinTimeHandler;
    private readonly IValidator<CreateSkillCommand> _validator;

    public CreateSkillCommandHandler(
        IUnitOfWork unitOfWork,
        IDbMinTimeHandler dbMinTimeHandler,
        IValidator<CreateSkillCommand> validator)
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
        CreateSkillCommand request,
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
                    await _unitOfWork.SkillRepository.AddAsync(
                        newEntity: Domain.Entities.Skill.Init(
                            skillId: Guid.NewGuid(),
                            skillName: request.NewSkillName,
                            skillRemovedAt: _dbMinTimeHandler.Get(),
                            skillRemovedBy: CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID),
                        cancellationToken: cancellationToken);

                    await _unitOfWork.SaveToDatabaseAsync(cancellationToken: cancellationToken);

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
