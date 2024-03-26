using FuDever.Domain.Specifications.Others.Interfaces;
using FuDever.Domain.UnitOfWorks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Skill.RemoveSkillPermanentlyBySkillId;

/// <summary>
///     Remove skill permanently by skill id request handler.
/// </summary>
internal sealed class RemoveSkillPermanentlyBySkillIdRequestHandler : IRequestHandler<
    RemoveSkillPermanentlyBySkillIdRequest,
    RemoveSkillPermanentlyBySkillIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public RemoveSkillPermanentlyBySkillIdRequestHandler(
        IUnitOfWork unitOfWork,
        ISuperSpecificationManager superSpecificationManager)
    {
        _unitOfWork = unitOfWork;
        _superSpecificationManager = superSpecificationManager;
    }

    /// <summary>
    ///     Entry of new request handler.
    /// </summary>
    /// <param name="request">
    ///     Request model.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     A task containing the response.
    /// </returns>
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
                StatusCode = RemoveSkillPermanentlyBySkillIdResponseStatusCode.SKILL_IS_NOT_FOUND
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
                StatusCode = RemoveSkillPermanentlyBySkillIdResponseStatusCode.SKILL_IS_NOT_TEMPORARILY_REMOVED
            };
        }

        // Remove skill permanently by skill id.
        var result = await RemoveSkillPermanentlyBySkillIdCommandAsync(
            request: request,
            cancellationToken: cancellationToken);

        // Database transaction false.
        if (!result)
        {
            return new()
            {
                StatusCode = RemoveSkillPermanentlyBySkillIdResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        return new()
        {
            StatusCode = RemoveSkillPermanentlyBySkillIdResponseStatusCode.OPERATION_SUCCESS
        };
    }

    #region Queries
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
    #endregion

    #region Commands
    /// <summary>
    ///     Attempt to removing skill permanently with new name.
    /// </summary>
    /// <param name="request">
    ///     Containing skill information.
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
        RemoveSkillPermanentlyBySkillIdRequest request,
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
                            _superSpecificationManager.UserSkill.UserSkillBySkillIdSpecification(skillId: request.SkillId),
                            _superSpecificationManager.UserSkill.SelectFieldsFromUserSkillSpecification.Ver2()
                        ],
                        cancellationToken: cancellationToken);

                    foreach (var foundUserSkill in foundUserSkills)
                    {
                        await _unitOfWork.UserRepository.BulkUpdateAsync(
                            specifications:
                            [
                                _superSpecificationManager.User.UserByIdSpecification(
                                    userId: foundUserSkill.UserId),
                                _superSpecificationManager.User.UpdateFieldOfUserSpecification.Ver2(
                                    userUpdatedAt: DateTime.UtcNow,
                                    userUpdatedBy: request.SkillRemovedBy)
                            ],
                            cancellationToken: cancellationToken);
                    }

                    await _unitOfWork.UserSkillRepository.BulkDeleteAsync(
                        specifications:
                        [
                            _superSpecificationManager.UserSkill.UserSkillBySkillIdSpecification(
                                skillId: request.SkillId)
                        ],
                        cancellationToken: cancellationToken);

                    await _unitOfWork.SkillRepository.BulkDeleteAsync(
                        specifications:
                        [
                            _superSpecificationManager.Skill.SkillByIdSpecification(
                                skillId: request.SkillId)
                        ],
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
    #endregion
}
