using Application.Interfaces.Caching;
using Domain.Specifications.Others.Interfaces;
using Domain.UnitOfWorks;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Skill.RemoveSkillTemporarilyBySkillId.Middlewares;

/// <summary>
///     Remove skill temporarily by skill
///     id request caching middleware.
/// </summary>
/// <remarks>
///     Order: 2nd
/// </remarks>
internal sealed class RemoveSkillTemporarilyBySkillIdCachingMiddleware :
    IPipelineBehavior<
        RemoveSkillTemporarilyBySkillIdRequest,
        RemoveSkillTemporarilyBySkillIdResponse>,
    IRemoveSkillTemporarilyBySkillIdMiddleware
{
    private readonly ICacheHandler _cacheHandler;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public RemoveSkillTemporarilyBySkillIdCachingMiddleware(
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
    public async Task<RemoveSkillTemporarilyBySkillIdResponse> Handle(
        RemoveSkillTemporarilyBySkillIdRequest request, RequestHandlerDelegate<RemoveSkillTemporarilyBySkillIdResponse> next,
        CancellationToken cancellationToken)
    {
        var response = await next();

        if (response.StatusCode == RemoveSkillTemporarilyBySkillIdStatusCode.OPERATION_SUCCESS)
        {
            var foundSkill = await _unitOfWork.SkillRepository.FindBySpecificationsAsync(
                specifications:
                [
                    _superSpecificationManager.Skill.SkillByIdSpecification(skillId: request.SkillId),
                    _superSpecificationManager.Skill.SelectFieldsFromSkillSpecification.Ver4()
                ],
                cancellationToken: cancellationToken);

            await Task.WhenAll(
                _cacheHandler.RemoveAsync(
                    key: $"{nameof(GetAllSkillsBySkillName)}_param_{foundSkill.Name.ToLower()}",
                    cancellationToken: cancellationToken),
                _cacheHandler.RemoveAsync(
                    key: nameof(GetAllSkills),
                    cancellationToken: cancellationToken),
                _cacheHandler.RemoveAsync(
                    key: nameof(GetAllTemporarilyRemovedSkills),
                    cancellationToken: cancellationToken));
        }

        return response;
    }
}
