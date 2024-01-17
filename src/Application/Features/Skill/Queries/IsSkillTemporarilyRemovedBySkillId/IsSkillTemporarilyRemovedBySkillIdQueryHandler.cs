using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Messaging;
using Domain.Specifications.Others.Interfaces;
using Domain.UnitOfWorks;

namespace Application.Features.Skill.Queries.IsSkillTemporarilyRemovedBySkillId;

/// <summary>
///     Is skill temporarily removed by skill id query handler.
/// </summary>
internal sealed class IsSkillTemporarilyRemovedBySkillIdQueryHandler : IQueryHandler<
    IsSkillTemporarilyRemovedBySkillIdQuery,
    bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    internal IsSkillTemporarilyRemovedBySkillIdQueryHandler(
        IUnitOfWork unitOfWork,
        ISuperSpecificationManager superSpecificationManager)
    {
        _unitOfWork = unitOfWork;
        _superSpecificationManager = superSpecificationManager;
    }

    /// <summary>
    ///     Entry of new query.
    /// </summary>
    /// <param name="request">
    ///     Query request modal.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     A task containing the boolean result.
    /// </returns>
    public Task<bool> Handle(
        IsSkillTemporarilyRemovedBySkillIdQuery request,
        CancellationToken cancellationToken)
    {
        if (request.SkillId == Guid.Empty)
        {
            return Task.FromResult(result: false);
        }

        var isSkillTemporarilyRemoved = _unitOfWork.SkillRepository.IsFoundBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Skill.SkillByIdSpecification(skillId: request.SkillId),
                _superSpecificationManager.Skill.SkillTemporarilyRemovedSpecification
            ],
            cancellationToken: cancellationToken);

        return isSkillTemporarilyRemoved;
    }
}
