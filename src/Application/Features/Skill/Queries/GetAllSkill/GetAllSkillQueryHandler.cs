using Application.Interfaces.Messaging;
using Domain.Specifications.Others.Interfaces;
using Domain.UnitOfWorks;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Skill.Queries.GetAllSkill;

/// <summary>
///     Get all skill query handler.
/// </summary>
internal sealed class GetAllSkillQueryHandler : IQueryHandler<
    GetAllSkillQuery,
    IEnumerable<Domain.Entities.Skill>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public GetAllSkillQueryHandler(
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
        GetAllSkillQuery request,
        CancellationToken cancellationToken)
    {
        var foundSkills = _unitOfWork.SkillRepository.GetAllBySpecificationsAsync(
            [
                _superSpecificationManager.Skill.SkillAsNoTrackingSpecification,
                _superSpecificationManager.Skill.SkillNotTemporarilyRemovedSpecification,
                _superSpecificationManager.Skill.SelectFieldsFromSkillSpecification.Ver1()
            ],
            cancellationToken);

        return foundSkills;
    }
}
