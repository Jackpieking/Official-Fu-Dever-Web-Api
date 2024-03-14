using FuDever.Application.Features.Skill.GetAllSkills;
using FuDever.Application.Features.Skill.GetAllSkillsBySkillName;
using FuDever.Application.Interfaces.Caching;
using FuDever.Domain.Specifications.Others.Interfaces;
using FuDever.Domain.UnitOfWorks;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Skill.UpdateSkillBySkillId.Middlewares;

/// <summary>
///     Update skill by skill id
///     request caching middleware.
/// </summary>
/// <remarks>
///     Order: 2nd
/// </remarks>
internal sealed class UpdateSkillBySkillIdCachingMiddleware :
    IPipelineBehavior<
        UpdateSkillBySkillIdRequest,
        UpdateSkillBySkillIdResponse>,
    IUpdateSkillBySkillIdMiddleware
{
    private readonly ICacheHandler _cacheHandler;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public UpdateSkillBySkillIdCachingMiddleware(
        ICacheHandler cacheHandler,
        IUnitOfWork unitOfWork,
        ISuperSpecificationManager superSpecificationManager)
    {
        _cacheHandler = cacheHandler;
        _unitOfWork = unitOfWork;
        _superSpecificationManager = superSpecificationManager;
    }

    /// <summary>
    ///     Entry to middleware handler.
    /// </summary>
    /// <param name="request">
    ///     Current request object.
    /// </param>
    /// <param name="next">
    ///     Navigate to next middleware and get back response.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     Response of use case.
    /// </returns>
    public async Task<UpdateSkillBySkillIdResponse> Handle(
        UpdateSkillBySkillIdRequest request,
        RequestHandlerDelegate<UpdateSkillBySkillIdResponse> next,
        CancellationToken cancellationToken)
    {
        // Finding current skill by skill id.
        var foundSkill = await _unitOfWork.SkillRepository.FindBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Skill.SkillByIdSpecification(skillId: request.SkillId),
                _superSpecificationManager.Skill.SelectFieldsFromSkillSpecification.Ver4()
            ],
            cancellationToken: cancellationToken);

        // Skill is found by skill id.
        if (!Equals(objA: foundSkill, objB: default))
        {
            await _cacheHandler.RemoveAsync(
                key: $"{nameof(GetAllSkillsBySkillNameRequest)}_param_{foundSkill.Name.ToLower()}",
                cancellationToken: cancellationToken);
        }

        var response = await next();

        if (response.StatusCode == UpdateSkillBySkillIdResponseStatusCode.OPERATION_SUCCESS)
        {
            await Task.WhenAll(
                _cacheHandler.RemoveAsync(
                    key: $"{nameof(GetAllSkillsBySkillNameRequest)}_param_{request.NewSkillName.ToLower()}",
                    cancellationToken: cancellationToken),
                _cacheHandler.RemoveAsync(
                    key: nameof(GetAllSkillsRequest),
                    cancellationToken: cancellationToken));
        }

        return response;
    }
}
