using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Specifications.Others.Interfaces;
using Domain.UnitOfWorks;
using MediatR;

namespace Application.Features.Skill.GetAllTemporarilyRemovedSkills;

/// <summary>
///     Get all temporarily removed skills request handler.
/// </summary>
internal sealed class GetAllTemporarilyRemovedSkillsHandler : IRequestHandler<
    GetAllTemporarilyRemovedSkillsRequest,
    GetAllTemporarilyRemovedSkillsResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public GetAllTemporarilyRemovedSkillsHandler(
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
        var foundTemporarilyRemovedSkills = await _unitOfWork.SkillRepository.GetAllBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Skill.SkillAsNoTrackingSpecification,
                _superSpecificationManager.Skill.SkillTemporarilyRemovedSpecification,
                _superSpecificationManager.Skill.SelectFieldsFromSkillSpecification.Ver2()
            ],
            cancellationToken: cancellationToken);

        // Project result to response.
        return new()
        {
            StatusCode = GetAllTemporarilyRemovedSkillsStatusCode.OPERATION_SUCCESS,
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
}
