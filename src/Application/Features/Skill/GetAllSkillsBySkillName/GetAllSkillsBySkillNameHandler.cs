using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Skill.GetAllSkillsBySkillName;
using Domain.Specifications.Others.Interfaces;
using Domain.UnitOfWorks;
using MediatR;

namespace Application.Features.Skill.GetAllSkillsByName;

/// <summary>
///     Get all skills by name request handler.
/// </summary>
internal sealed class GetAllSkillBySkillNameHandler : IRequestHandler<
    GetAllSkillsBySkillNameRequest,
    GetAllSkillsBySkillNameResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public GetAllSkillBySkillNameHandler(
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
    public async Task<GetAllSkillsBySkillNameResponse> Handle(
        GetAllSkillsBySkillNameRequest request,
        CancellationToken cancellationToken)
    {
        // Get all skills by name.
        var foundSkills = await _unitOfWork.SkillRepository.GetAllBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Skill.SkillAsNoTrackingSpecification,
                    _superSpecificationManager.Skill.SkillByNameSpecification(
                        skillName: request.SkillName,
                        isCaseSensitive: false),
                    _superSpecificationManager.Skill.SkillNotTemporarilyRemovedSpecification,
                    _superSpecificationManager.Skill.SelectFieldsFromSkillSpecification.Ver1()
            ],
            cancellationToken: cancellationToken);

        return new()
        {
            StatusCode = GetAllSkillsBySkillNameStatusCode.OPERATION_SUCCESS,
            FoundSkills = foundSkills.Select(selector: foundSkill =>
            {
                return new GetAllSkillsBySkillNameResponse.Skill()
                {
                    SkillId = foundSkill.Id,
                    SkillName = foundSkill.Name
                };
            }),
        };
    }
}
