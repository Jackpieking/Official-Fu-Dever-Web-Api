using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Specifications.Others.Interfaces;
using Domain.UnitOfWorks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Skill.UpdateSkillBySkillId;

/// <summary>
///     Update skill by skill id request handler.
/// </summary>
internal sealed class UpdateSkillBySkillIdHandler : IRequestHandler<
    UpdateSkillBySkillIdRequest,
    UpdateSkillBySkillIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public UpdateSkillBySkillIdHandler(
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
                StatusCode = UpdateSkillBySkillIdStatusCode.SKILL_IS_NOT_FOUND
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
                StatusCode = UpdateSkillBySkillIdStatusCode.SKILL_IS_ALREADY_TEMPORARILY_REMOVED
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
                StatusCode = UpdateSkillBySkillIdStatusCode.SKILL_ALREADY_EXISTS
            };
        }

        // Update skill by skill id.
        var result = await UpdateSkillBySkillIdCommandAsync(
            skillId: request.SkillId,
            newSkillName: request.NewSkillName,
            skillUpdatedBy: request.SkillUpdatedBy,
            cancellationToken: cancellationToken);

        // Database transaction false.
        if (!result)
        {
            return new()
            {
                StatusCode = UpdateSkillBySkillIdStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        return new()
        {
            StatusCode = UpdateSkillBySkillIdStatusCode.OPERATION_SUCCESS
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

    // === Commands ===

    /// <summary>
    ///     Attempt to update skill with new name by skill id.
    /// </summary>
    /// <param name="skillId">
    ///     Skill id.
    /// </param>
    /// <param name="newSkillName">
    ///     New skill name.
    /// </param>
    /// <param name="skillUpdatedBy">
    ///     Who update the skill.
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
        Guid skillId,
        string newSkillName,
        Guid skillUpdatedBy,
        CancellationToken cancellationToken)
    {
        // Start updating skill transaction.
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
                            _superSpecificationManager.UserSkill.SelectFieldsFromUserSkillSpecification.Ver2(),
                        ],
                        cancellationToken: cancellationToken);

                    foreach (var foundUserSkill in foundUserSkills)
                    {
                        await _unitOfWork.UserRepository.BulkUpdateByUserIdVer1Async(
                            userId: foundUserSkill.UserId,
                            userUpdatedAt: DateTime.UtcNow,
                            userUpdatedBy: skillUpdatedBy,
                            cancellationToken: cancellationToken);
                    }

                    await _unitOfWork.SkillRepository.BulkUpdateBySkillIdVer2Async(
                        skillId: skillId,
                        skillName: newSkillName,
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
