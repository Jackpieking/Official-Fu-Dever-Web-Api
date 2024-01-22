using Application.Interfaces.Messaging;
using Domain.Specifications.Others.Interfaces;
using Domain.UnitOfWorks;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Skill.Queries.GetAllTemporarilyRemovedSkill;

/// <summary>
///     Get all temporarily removed skill query handler.
/// </summary>
internal sealed class GetAllTemporarilyRemovedSkillQueryHandler : IQueryHandler<
    GetAllTemporarilyRemovedSkillQuery,
    IEnumerable<Domain.Entities.Skill>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public GetAllTemporarilyRemovedSkillQueryHandler(
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
    public Task<IEnumerable<Domain.Entities.Skill>> Handle(
        GetAllTemporarilyRemovedSkillQuery request,
        CancellationToken cancellationToken)
    {
        var foundTemporaryRemovedSkills = _unitOfWork.SkillRepository.GetAllBySpecificationsAsync(
            [
                _superSpecificationManager.Skill.SkillAsNoTrackingSpecification,
                _superSpecificationManager.Skill.SkillTemporarilyRemovedSpecification,
                _superSpecificationManager.Skill.SelectFieldsFromSkillSpecification.Ver2()
            ],
            cancellationToken);

        return foundTemporaryRemovedSkills;
    }
}
