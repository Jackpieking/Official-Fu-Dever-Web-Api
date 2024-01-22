using Application.Interfaces.Messaging;
using Domain.Entities;
using Domain.Specifications.Others.Interfaces;
using Domain.UnitOfWorks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Skill.Commands.RemoveSkillPermanently;

/// <summary>
///     Remove skill permanently command handler.
/// </summary>
internal sealed class RemoveSkillPermanentlyCommandHandler : ICommandHandler<
    RemoveSkillPermanentlyCommand,
    bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;
    private readonly IValidator<RemoveSkillPermanentlyCommand> _validator;

    public RemoveSkillPermanentlyCommandHandler(
        IUnitOfWork unitOfWork,
        ISuperSpecificationManager superSpecificationManager,
        IValidator<RemoveSkillPermanentlyCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _superSpecificationManager = superSpecificationManager;
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
        RemoveSkillPermanentlyCommand request,
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
                    var foundUserSkills = await _unitOfWork.UserSkillRepository.GetAllBySpecificationsAsync(
                        specifications:
                        [
                            _superSpecificationManager.UserSkill.UserSkillBySkillIdSpecification(skillId: request.SkillId),
                            _superSpecificationManager.UserSkill.SelectFieldsFromUserSkillSpecification.Ver2()
                        ],
                        cancellationToken: cancellationToken);

                    await SetUpdateSectionOfUserToLatestAsync(
                        foundUserSkills: foundUserSkills,
                        skillRemovedBy: request.SkillRemovedBy,
                        cancellationToken: cancellationToken);

                    await _unitOfWork.UserSkillRepository.BulkRemoveBySkillIdAsync(
                        skillId: request.SkillId,
                        cancellationToken: cancellationToken);

                    await _unitOfWork.SkillRepository.BulkRemoveBySkillIdAsync(
                        skillId: request.SkillId,
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

    /// <summary>
    ///     Set the update section (UpdatedBy, UpdatedAt) of
    ///     user to the latest.
    /// </summary>
    /// <param name="foundUserSkills">
    ///     List of user skill to extract user id.
    /// </param>
    /// <param name="skillRemovedBy">
    ///     Who remove the skill.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     Task containing the result of operation.
    /// </returns>
    private async Task SetUpdateSectionOfUserToLatestAsync(
        IEnumerable<UserSkill> foundUserSkills,
        Guid skillRemovedBy,
        CancellationToken cancellationToken)
    {
        foreach (var foundUserSkill in foundUserSkills)
        {
            await _unitOfWork.UserRepository.BulkUpdateByUserIdVer1Async(
                userId: foundUserSkill.UserId,
                userUpdatedAt: DateTime.UtcNow,
                userUpdatedBy: skillRemovedBy,
                cancellationToken: cancellationToken);
        }
    }
}
