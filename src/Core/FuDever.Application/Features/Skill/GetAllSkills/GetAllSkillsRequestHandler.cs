using FuDever.Domain.Specifications.Others.Interfaces;
using FuDever.Domain.UnitOfWorks;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Skill.GetAllSkills;

/// <summary>
///     Get all skills request handler.
/// </summary>
internal sealed class GetAllSkillsRequestHandler : IRequestHandler<
    GetAllSkillsRequest,
    GetAllSkillsResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public GetAllSkillsRequestHandler(
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
        // Get all non temporarily removed skills.
        var foundSkills = await GetAllNonTemporarilyRemovedSkillsQueryAsync(cancellationToken: cancellationToken);

        // Project result to response.
        return new()
        {
            StatusCode = GetAllSkillsResponseStatusCode.OPERATION_SUCCESS,
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

    #region Queries
    /// <summary>
    ///     Get all skills which are not temporarily removed.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     List of found skills.
    /// </returns>
    private Task<IEnumerable<Domain.Entities.Skill>> GetAllNonTemporarilyRemovedSkillsQueryAsync(CancellationToken cancellationToken)
    {
        return _unitOfWork.SkillRepository.GetAllBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Skill.SkillAsNoTrackingSpecification,
                _superSpecificationManager.Skill.SkillNotTemporarilyRemovedSpecification,
                _superSpecificationManager.Skill.SkillNameIsNotDefaultSpecification,
                _superSpecificationManager.Skill.SelectFieldsFromSkillSpecification.Ver1()
            ],
            cancellationToken: cancellationToken);
    }
    #endregion
}
