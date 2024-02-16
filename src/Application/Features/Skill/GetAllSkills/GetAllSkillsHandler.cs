using Domain.Specifications.Others.Interfaces;
using Domain.UnitOfWorks;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Skill.GetAllSkills;

/// <summary>
///     Get all skills request handler.
/// </summary>
internal sealed class GetAllSkillsHandler : IRequestHandler<
    GetAllSkillsRequest,
    GetAllSkillsResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public GetAllSkillsHandler(
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
    public async Task<GetAllSkillsResponse> Handle(
        GetAllSkillsRequest request,
        CancellationToken cancellationToken)
    {
        // Get all skills.
        var foundSkills = await _unitOfWork.SkillRepository.GetAllBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Skill.SkillAsNoTrackingSpecification,
                _superSpecificationManager.Skill.SkillNotTemporarilyRemovedSpecification,
                _superSpecificationManager.Skill.SelectFieldsFromSkillSpecification.Ver1()
            ],
            cancellationToken: cancellationToken);

        // Project result to response.
        return new()
        {
            StatusCode = GetAllSkillsStatusCode.OPERATION_SUCCESS,
            FoundSkills = foundSkills.Select(selector: foundSkill =>
            {
                return new GetAllSkillsResponse.Skill()
                {
                    SkillId = foundSkill.Id,
                    SkillName = foundSkill.Name
                };
            })
        };
    }
}
