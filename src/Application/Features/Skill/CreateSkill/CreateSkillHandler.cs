using Application.Commons;
using Application.Interfaces.Data;
using Domain.Specifications.Others.Interfaces;
using Domain.UnitOfWorks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Skill.CreateSkill;

/// <summary>
///     Create skill request handler.
/// </summary>
internal sealed class CreateSkillHandler : IRequestHandler<
    CreateSkillRequest,
    CreateSkillResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;
    private readonly IDbMinTimeHandler _dbMinTimeHandler;

    public CreateSkillHandler(
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
    public async Task<CreateSkillResponse> Handle(
        CreateSkillRequest request,
        CancellationToken cancellationToken)
    {
        // Is skill with the same skill name found.
        var isSKillFound = await IsSkillWithTheSameNameFoundBySkillNameQueryAsync(
            newSkillName: request.NewSkillName,
            cancellationToken: cancellationToken);

        // Skills with the same skill name is found.
        if (isSKillFound)
        {
            // Is skill temporarily removed by skill name.
            var isSkillTemporarilyRemoved = await IsSkillTemporarilyRemovedBySkillNameQueryAsync(
                newSkillName: request.NewSkillName,
                cancellationToken: cancellationToken);

            // Skill with skill name is already temporarily removed.
            if (isSkillTemporarilyRemoved)
            {
                return new()
                {
                    StatusCode = CreateSkillStatusCode.SKILL_IS_ALREADY_TEMPORARILY_REMOVED
                };
            }

            return new()
            {
                StatusCode = CreateSkillStatusCode.SKILL_ALREADY_EXISTS
            };
        }

        // Create skill with new skill name.
        var result = await CreateSkillCommandAsync(
            newSkillName: request.NewSkillName,
            cancellationToken: cancellationToken);

        // Database transaction false.
        if (!result)
        {
            return new()
            {
                StatusCode = CreateSkillStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        return new()
        {
            StatusCode = CreateSkillStatusCode.OPERATION_SUCCESS
        };
    }

    // === Queries ===

    /// <summary>
    ///     Is skill having the same name with
    ///     the new one found.
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

    /// <summary>
    ///     Is skill temporarily removed by skill name.
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
    ///     True if skill already temporarily removed. Otherwise, false.
    /// </returns>
    private Task<bool> IsSkillTemporarilyRemovedBySkillNameQueryAsync(
        string newSkillName,
        CancellationToken cancellationToken)
    {
        return _unitOfWork.SkillRepository.IsFoundBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Skill.SkillByNameSpecification(
                    skillName: newSkillName,
                    isCaseSensitive: true),
                _superSpecificationManager.Skill.SkillTemporarilyRemovedSpecification
            ],
            cancellationToken: cancellationToken);
    }

    // === Commands ===

    /// <summary>
    ///     Attempt to creating a new skill with the
    ///     given name and add to database.
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
    ///     True if adding skill operation is successful.
    ///     Otherwise, false.
    /// </returns>
    private async Task<bool> CreateSkillCommandAsync(
        string newSkillName,
        CancellationToken cancellationToken)
    {
        // Start adding entity transaction.
        var executedTransactionResult = false;

        await _unitOfWork
            .CreateExecutionStrategy()
            .ExecuteAsync(operation: async () =>
            {
                try
                {
                    await _unitOfWork.CreateTransactionAsync(cancellationToken: cancellationToken);

                    await _unitOfWork.SkillRepository.AddAsync(
                        newEntity: Domain.Entities.Skill.InitVer1(
                            skillId: Guid.NewGuid(),
                            skillName: newSkillName,
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
