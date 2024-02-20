using Application.Commons;
using Application.Interfaces.Data;
using Domain.Specifications.Others.Interfaces;
using Domain.UnitOfWorks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Skill.RestoreSkillBySkillId;

/// <summary>
///     Restore skill by skill id request handler.
/// </summary>
internal sealed class RestoreSkillBySkillIdHandler : IRequestHandler<
    RestoreSkillBySkillIdRequest,
    RestoreSkillBySkillIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;
    private readonly IDbMinTimeHandler _dbMinTimeHandler;

    public RestoreSkillBySkillIdHandler(
        IUnitOfWork unitOfWork,
        ISuperSpecificationManager superSpecificationManager,
        IDbMinTimeHandler dbMinTimeHandler)
    {
        _unitOfWork = unitOfWork;
        _superSpecificationManager = superSpecificationManager;
        _dbMinTimeHandler = dbMinTimeHandler;
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

    public async Task<RestoreSkillBySkillIdResponse> Handle(
        RestoreSkillBySkillIdRequest request,
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
                StatusCode = RestoreSkillBySkillIdStatusCode.SKILL_IS_NOT_FOUND
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
                StatusCode = RestoreSkillBySkillIdStatusCode.SKILL_IS_NOT_TEMPORARILY_REMOVED
            };
        }

        // Restore skill by skill id.
        var result = await RestoreSkillBySkillIdCommandAsync(
            request: request,
            cancellationToken: cancellationToken);

        // Database transaction false.
        if (!result)
        {
            return new()
            {
                StatusCode = RestoreSkillBySkillIdStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        return new()
        {
            StatusCode = RestoreSkillBySkillIdStatusCode.OPERATION_SUCCESS
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
    ///     Attempt to restore skill by skill id.
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
    ///     True if restoring skill permanently operation is
    ///     successful. Otherwise, false.
    /// </returns>
    public async Task<bool> RestoreSkillBySkillIdCommandAsync(
        RestoreSkillBySkillIdRequest request,
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

                    await _unitOfWork.SkillRepository.BulkUpdateBySkillIdVer1Async(
                        skillId: request.SkillId,
                        skillRemovedAt: _dbMinTimeHandler.Get(),
                        skillRemovedBy: CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID,
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
