using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Specifications.Others.Interfaces;
using Domain.UnitOfWorks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Skill.RemoveSkillPermanentlyBySkillId;

/// <summary>
///     Remove skill permanently by skill id request handler.
/// </summary>
internal sealed class RemoveSkillPermanentlyBySkillIdHandler : IRequestHandler<
    RemoveSkillPermanentlyBySkillIdRequest,
    RemoveSkillPermanentlyBySkillIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public RemoveSkillPermanentlyBySkillIdHandler(
        IUnitOfWork unitOfWork,
        ISuperSpecificationManager superSpecificationManager)
    {
        _unitOfWork = unitOfWork;
        _superSpecificationManager = superSpecificationManager;
    }

    public async Task<RemoveSkillPermanentlyBySkillIdResponse> Handle(
        RemoveSkillPermanentlyBySkillIdRequest request,
        CancellationToken cancellationToken)
    {
        // Is skill found by skill id.
        var isSkillFoundBySkillId = await IsSkillFoundBySkillIdQueryAsync(
            skillId: request.SkillId,
            cancellationToken: cancellationToken);

        // Skill is not found by skill id.
        if (!isSkillFoundBySkillId)
        {
            return new()
            {
                StatusCode = RemoveSkillPermanentlyBySkillIdStatusCode.SKILL_IS_NOT_FOUND
            };
        }

        // Is skill temporarily removed by skill id.
        var isSkillTemporarilyRemoved = await IsSkillTemporarilyRemovedBySkillIdQueryAsync(
            skillId: request.SkillId,
            cancellationToken: cancellationToken);

        // Skill is not temporarily removed by skill id.
        if (!isSkillTemporarilyRemoved)
        {
            return new()
            {
                StatusCode = RemoveSkillPermanentlyBySkillIdStatusCode.SKILL_IS_NOT_TEMPORARILY_REMOVED
            };
        }

        // Remove skill permanently by skill id.
        var result = await RemoveSkillPermanentlyBySkillIdCommandAsync(
            skillId: request.SkillId,
            skillRemovedBy: request.SkillRemovedBy,
            cancellationToken: cancellationToken);

        // Database transaction false.
        if (!result)
        {
            return new()
            {
                StatusCode = RemoveSkillPermanentlyBySkillIdStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        return new()
        {
            StatusCode = RemoveSkillPermanentlyBySkillIdStatusCode.OPERATION_SUCCESS
        };
    }

    // === Queries ===

    /// <summary>
    ///     Is skill found by skill id.
    /// </summary>
    /// <param name="skillId">
    ///     Skill id.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     True if skill is found by skill
    ///     id. Otherwise, false.
    /// </returns>
    private Task<bool> IsSkillFoundBySkillIdQueryAsync(
        Guid skillId,
        CancellationToken cancellationToken)
    {
        return _unitOfWork.SkillRepository.IsFoundBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Skill.SkillByIdSpecification(skillId: skillId),
            ],
            cancellationToken: cancellationToken);
    }

    /// <summary>
    ///     Is skill temporarily removed by skill id.
    /// </summary>
    /// <param name="skillId">
    ///     Skill id.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     True if skill is temporarily removed by skill id.
    ///     Otherwise, false.
    /// </returns>
    private Task<bool> IsSkillTemporarilyRemovedBySkillIdQueryAsync(
        Guid skillId,
        CancellationToken cancellationToken)
    {
        return _unitOfWork.SkillRepository.IsFoundBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Skill.SkillByIdSpecification(skillId: skillId),
                _superSpecificationManager.Skill.SkillTemporarilyRemovedSpecification
            ],
            cancellationToken: cancellationToken);
    }

    // === Commands ===

    /// <summary>
    ///     Attempt to removing skill permanently with new name.
    /// </summary>
    /// <param name="skillId">
    ///     Skill id.
    /// </param>
    /// <param name="skillRemovedBy">
    ///     Who remove the skill permanently.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     True if removing skill permanently operation is
    ///     successful. Otherwise, false.
    /// </returns>
    private async Task<bool> RemoveSkillPermanentlyBySkillIdCommandAsync(
        Guid skillId,
        Guid skillRemovedBy,
        CancellationToken cancellationToken)
    {
        var executedTransactionResult = false;

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
                            _superSpecificationManager.UserSkill.UserSkillAsNoTrackingSpecification,
                            _superSpecificationManager.UserSkill.UserSkillBySkillIdSpecification(skillId: skillId),
                            _superSpecificationManager.UserSkill.SelectFieldsFromUserSkillSpecification.Ver2()
                        ],
                        cancellationToken: cancellationToken);

                    foreach (var foundUserSkill in foundUserSkills)
                    {
                        await _unitOfWork.UserRepository.BulkUpdateByUserIdVer1Async(
                            userId: foundUserSkill.UserId,
                            userUpdatedAt: DateTime.UtcNow,
                            userUpdatedBy: skillRemovedBy,
                            cancellationToken: cancellationToken);
                    }

                    await _unitOfWork.UserSkillRepository.BulkRemoveBySkillIdAsync(
                        skillId: skillId,
                        cancellationToken: cancellationToken);

                    await _unitOfWork.SkillRepository.BulkRemoveBySkillIdAsync(
                        skillId: skillId,
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
