using Application.Commons;
using Application.Features.Skill.Queries.FindBySkillName;
using Application.Features.Skill.Queries.GetAllSkill;
using Application.Features.Skill.Queries.IsSkillFoundBySkillName;
using Application.Interfaces.Caching;
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
    private readonly ICacheHandler _cacheHandler;

    public CreateSkillCommandHandler(
        IUnitOfWork unitOfWork,
        IDbMinTimeHandler dbMinTimeHandler,
        IValidator<CreateSkillCommand> validator,
        ICacheHandler cacheHandler)
    {
        _unitOfWork = unitOfWork;
        _dbMinTimeHandler = dbMinTimeHandler;
        _validator = validator;
        _cacheHandler = cacheHandler;
    }

    /// <summary>
    ///     Entry of new command.
    /// </summary>
    /// <param name="request">
    ///     Command request model.
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
        // Validate input.
        var inputValidationResult = await _validator.ValidateAsync(
            instance: request,
            cancellation: cancellationToken);

        if (!inputValidationResult.IsValid)
        {
            return false;
        }

        // Remove related cache values.
        await ClearCacheAsync(
            request: request,
            cancellationToken: cancellationToken);

        // Start adding entity transaction.
        return await ExecuteTransactionAsync(
            request: request,
            cancellationToken: cancellationToken);
    }

    /// <summary>
    ///     Clear all cache values that
    ///     are related to this action.
    /// </summary>
    /// <param name="request">
    ///     Model of the request.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     The task that has no value.
    /// </returns>
    private async Task ClearCacheAsync(
        CreateSkillCommand request,
        CancellationToken cancellationToken)
    {
        await Task.WhenAll(
            _cacheHandler.RemoveAsync(
                key: $"{nameof(FindBySkillNameQueryHandler)}_request_{request.NewSkillName.ToLower()}",
                cancellationToken: cancellationToken),
            _cacheHandler.RemoveAsync(
                key: $"{nameof(GetAllSkillQueryHandler)}_request",
                cancellationToken: cancellationToken),
            _cacheHandler.RemoveAsync(
                key: $"{nameof(IsSkillFoundBySkillNameQueryHandler)}_request_{request.NewSkillName}",
                cancellationToken: cancellationToken));
    }

    /// <summary>
    ///     Execute the transaction of the main database.
    /// </summary>
    /// <param name="request">
    ///     Model of the request.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     True if transaction is success. Otherwise, false.
    /// </returns>
    private async Task<bool> ExecuteTransactionAsync(
        CreateSkillCommand request,
        CancellationToken cancellationToken)
    {
        // Start adding entity transaction.
        var executedTransactionResult = default(bool);

        await _unitOfWork
            .CreateExecutionStrategy()
            .ExecuteAsync(operation: async () =>
            {
                try
                {
                    await _unitOfWork.CreateTransactionAsync(cancellationToken: cancellationToken);

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
