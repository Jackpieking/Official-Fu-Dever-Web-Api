using FuDever.Domain.Specifications.Others.Interfaces;
using FuDever.Domain.UnitOfWorks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Skill.UpdateSkillBySkillId;

/// <summary>
///     Update skill by skill id request handler.
/// </summary>
internal sealed class UpdateSkillBySkillIdRequestHandler : IRequestHandler<
    UpdateSkillBySkillIdRequest,
    UpdateSkillBySkillIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public UpdateSkillBySkillIdRequestHandler(
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
    public async Task<UpdateSkillBySkillIdResponse> Handle(
        UpdateSkillBySkillIdRequest request,
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
                StatusCode = UpdateSkillBySkillIdResponseStatusCode.SKILL_IS_NOT_FOUND
            };
        }

        // Is skill temporarily removed by skill id.
        var isSkillTemporarilyRemoved = await IsSkillTemporarilyRemovedBySkillIdQueryAsync(
            skillId: request.SkillId,
            cancellationToken: cancellationToken);

        // Skill is already temporarily removed by skill id.
        if (isSkillTemporarilyRemoved)
        {
            return new()
            {
                StatusCode = UpdateSkillBySkillIdResponseStatusCode.SKILL_IS_ALREADY_TEMPORARILY_REMOVED
            };
        }

        // Is skill with the same skill name found.
        var isSkillWithTheSameNameFound = await IsSkillWithTheSameNameFoundBySkillNameQueryAsync(
            newSkillName: request.NewSkillName,
            cancellationToken: cancellationToken);

        // Skill with the same skill name is found.
        if (isSkillWithTheSameNameFound)
        {
            return new()
            {
                StatusCode = UpdateSkillBySkillIdResponseStatusCode.SKILL_ALREADY_EXISTS
            };
        }

        // Update skill by skill id.
        var result = await UpdateSkillBySkillIdCommandAsync(
            request: request,
            cancellationToken: cancellationToken);

        // Database transaction false.
        if (!result)
        {
            return new()
            {
                StatusCode = UpdateSkillBySkillIdResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        return new()
        {
            StatusCode = UpdateSkillBySkillIdResponseStatusCode.OPERATION_SUCCESS
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

    /// <summary>
    ///     Is skill found by skill name in
    ///     the in-sensitive way.
    /// </summary>
    /// <param name="newSkillName">
    ///     New skill name.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     True if skill already exists. Otherwise, false.
    /// </returns>
    private Task<bool> IsSkillWithTheSameNameFoundBySkillNameQueryAsync(
        string newSkillName,
        CancellationToken cancellationToken)
    {
        return _unitOfWork.SkillRepository.IsFoundBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Skill.SkillByNameSpecification(
                    skillName: newSkillName,
                    isCaseSensitive: true),
            ],
            cancellationToken: cancellationToken);
    }
    #endregion

    #region Commands
    /// <summary>
    ///     Attempt to update skill with new name by skill id.
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
    ///     True if updating skill operation is successful.
    ///     Otherwise, false.
    /// </returns>
    private async Task<bool> UpdateSkillBySkillIdCommandAsync(
        UpdateSkillBySkillIdRequest request,
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

                    await _unitOfWork.UserSkillRepository.BulkUpdateAsync(
                        specifications:
                        [
                            _superSpecificationManager.UserSkill.UserSkillBySkillIdSpecification(
                                skillId: request.SkillId),
                            _superSpecificationManager.UserSkill.UpdateFieldOfUserSkillSpecification.Ver1(
                                userUpdatedAt: DateTime.UtcNow,
                                userUpdatedBy: request.SkillUpdatedBy)
                        ],
                        cancellationToken: cancellationToken);

                    await _unitOfWork.SkillRepository.BulkUpdateAsync(
                        specifications:
                        [
                            _superSpecificationManager.Skill.SkillByIdSpecification(
                                skillId: request.SkillId),
                            _superSpecificationManager.Skill.UpdateFieldOfSkillSpecification.Ver2(
                                skillName: request.NewSkillName)
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
