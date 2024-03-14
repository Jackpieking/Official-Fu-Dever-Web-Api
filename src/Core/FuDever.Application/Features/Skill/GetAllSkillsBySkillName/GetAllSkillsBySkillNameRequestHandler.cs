using FuDever.Domain.Specifications.Others.Interfaces;
using FuDever.Domain.UnitOfWorks;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Skill.GetAllSkillsBySkillName;

/// <summary>
///     Get all skills by name request handler.
/// </summary>
internal sealed class GetAllSkillsBySkillNameRequestHandler : IRequestHandler<
    GetAllSkillsBySkillNameRequest,
    GetAllSkillsBySkillNameResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public GetAllSkillsBySkillNameRequestHandler(
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
    /// </param>
    /// <returns>
    ///     A task containing the boolean result.
    /// </returns>
    public async Task<GetAllSkillsBySkillNameResponse> Handle(
        GetAllSkillsBySkillNameRequest request,
        CancellationToken cancellationToken)
    {
        // Get all skills by name.
        var foundSkills = await GetAllSkillsBySkillNameQueryAsync(
            skillName: request.SkillName,
            cancellationToken: cancellationToken);

        return new()
        {
            StatusCode = GetAllSkillsBySkillNameResponseStatusCode.OPERATION_SUCCESS,
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

    #region Queries
    /// <summary>
    ///     Get all skill by skill name
    /// </summary>
    /// <param name="skillName">
    ///     Skill name to find.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     List of found skills.
    /// </returns>
    private Task<IEnumerable<Domain.Entities.Skill>> GetAllSkillsBySkillNameQueryAsync(
        string skillName,
        CancellationToken cancellationToken)
    {
        return _unitOfWork.SkillRepository.GetAllBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Skill.SkillAsNoTrackingSpecification,
                _superSpecificationManager.Skill.SkillByNameSpecification(
                        skillName: skillName,
                        isCaseSensitive: false),
                _superSpecificationManager.Skill.SkillNotTemporarilyRemovedSpecification,
                _superSpecificationManager.Skill.SelectFieldsFromSkillSpecification.Ver1()
            ],
            cancellationToken: cancellationToken);
    }
    #endregion
}
