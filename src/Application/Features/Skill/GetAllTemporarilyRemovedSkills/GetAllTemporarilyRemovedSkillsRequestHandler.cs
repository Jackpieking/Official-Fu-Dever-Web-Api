using Domain.Specifications.Others.Interfaces;
using Domain.UnitOfWorks;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Skill.GetAllTemporarilyRemovedSkills;

/// <summary>
///     Get all temporarily removed skills request handler.
/// </summary>
internal sealed class GetAllTemporarilyRemovedSkillsRequestHandler : IRequestHandler<
    GetAllTemporarilyRemovedSkillsRequest,
    GetAllTemporarilyRemovedSkillsResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public GetAllTemporarilyRemovedSkillsRequestHandler(
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
    ///     A task containing the boolean result.
    /// </returns>
    public async Task<GetAllTemporarilyRemovedSkillsResponse> Handle(
        GetAllTemporarilyRemovedSkillsRequest request,
        CancellationToken cancellationToken)
    {
        // Get all temporarily removed skills.
        var foundTemporarilyRemovedSkills = await GetAllTemporarilyRemovedSkillsQueryAsync(cancellationToken: cancellationToken);

        // Project result to response.
        return new()
        {
            StatusCode = GetAllTemporarilyRemovedSkillsResponseStatusCode.OPERATION_SUCCESS,
            FoundTemporarilyRemovedSkills = foundTemporarilyRemovedSkills.Select(selector: foundSkill =>
            {
                return new GetAllTemporarilyRemovedSkillsResponse.Skill()
                {
                    SkillId = foundSkill.Id,
                    SkillName = foundSkill.Name,
                    SkillRemovedAt = foundSkill.RemovedAt,
                    SkillRemovedBy = foundSkill.RemovedBy
                };
            })
        };
    }

    #region Queries
    /// <summary>
    ///     Get all skills which are temporarily removed.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     List of found skills.
    /// </returns>
    private Task<IEnumerable<Domain.Entities.Skill>> GetAllTemporarilyRemovedSkillsQueryAsync(CancellationToken cancellationToken)
    {
        return _unitOfWork.SkillRepository.GetAllBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Skill.SkillAsNoTrackingSpecification,
                _superSpecificationManager.Skill.SkillTemporarilyRemovedSpecification,
                _superSpecificationManager.Skill.SelectFieldsFromSkillSpecification.Ver2()
            ],
            cancellationToken: cancellationToken);
    }
    #endregion
}
