using Application.Features.Skill.Queries.FindBySkillName;
using Application.Features.Skill.Queries.GetAllSkill;
using Application.Features.Skill.Queries.GetAllTemporarilyRemovedSkill;
using Application.Features.Skill.Queries.IsSkillFoundBySkillName;
using Application.Features.Skill.Queries.IsSkillTemporarilyRemovedBySkillName;
using Application.Interfaces.Caching;
using Application.Interfaces.Messaging;
using Domain.Specifications.Others.Interfaces;
using Domain.UnitOfWorks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Skill.Commands.UpdateSkill;

/// <summary>
///     Update skill command handler.
/// </summary>
internal sealed class UpdateSkillCommandHandler : ICommandHandler<
    UpdateSkillCommand,
    bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;
    private readonly IValidator<UpdateSkillCommand> _validator;
    private readonly ICacheHandler _cacheHandler;

    public UpdateSkillCommandHandler(
        IUnitOfWork unitOfWork,
        ISuperSpecificationManager superSpecificationManager,
        IValidator<UpdateSkillCommand> validator,
        ICacheHandler cacheHandler)
    {
        _unitOfWork = unitOfWork;
        _superSpecificationManager = superSpecificationManager;
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
        UpdateSkillCommand request,
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

        // Start removing skill permanently transaction.
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
        UpdateSkillCommand request,
        CancellationToken cancellationToken)
    {
        var foundSkill = await _unitOfWork.SkillRepository.FindBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Skill.SkillByIdSpecification(
                    skillId: request.SkillId),
                _superSpecificationManager.Skill.SelectFieldsFromSkillSpecification.Ver4()
            ],
            cancellationToken: cancellationToken);

        // Remove related cache values.
        await Task.WhenAll(
            _cacheHandler.RemoveAsync(
                key: $"{nameof(FindBySkillNameQueryHandler)}_request_{foundSkill.Name.ToLower()}",
                cancellationToken: cancellationToken),
            _cacheHandler.RemoveAsync(
                key: $"{nameof(GetAllSkillQueryHandler)}_request",
                cancellationToken: cancellationToken),
            _cacheHandler.RemoveAsync(
                key: $"{nameof(GetAllTemporarilyRemovedSkillQueryHandler)}_request",
                cancellationToken: cancellationToken),
            _cacheHandler.RemoveAsync(
                key: $"{nameof(IsSkillFoundBySkillNameQueryHandler)}_request_{foundSkill.Name}",
                cancellationToken: cancellationToken),
            _cacheHandler.RemoveAsync(
                key: $"{nameof(IsSkillTemporarilyRemovedBySkillNameQueryHandler)}_request_{foundSkill.Name}",
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
    public async Task<bool> ExecuteTransactionAsync(
        UpdateSkillCommand request,
        CancellationToken cancellationToken)
    {
        // Start updating skill transaction.
        var executedTransactionResult = default(bool);

        await _unitOfWork
            .CreateExecutionStrategy()
            .ExecuteAsync(operation: async () =>
            {
                try
                {
                    await _unitOfWork.CreateTransactionAsync(cancellationToken: cancellationToken);

                    var foundUserSkills = await _unitOfWork.UserSkillRepository.GetAllBySpecificationsAsync(
                        specifications:
                        [
                            _superSpecificationManager.UserSkill.UserSkillBySkillIdSpecification(skillId: request.SkillId),
                            _superSpecificationManager.UserSkill.SelectFieldsFromUserSkillSpecification.Ver2(),
                        ],
                        cancellationToken: cancellationToken);

                    foreach (var foundUserSkill in foundUserSkills)
                    {
                        await _unitOfWork.UserRepository.BulkUpdateByUserIdVer1Async(
                            userId: foundUserSkill.UserId,
                            userUpdatedAt: DateTime.UtcNow,
                            userUpdatedBy: request.SkillUpdatedBy,
                            cancellationToken: cancellationToken);
                    }

                    await _unitOfWork.SkillRepository.BulkUpdateBySkillIdVer2Async(
                        skillId: request.SkillId,
                        skillName: request.NewSkillName,
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
