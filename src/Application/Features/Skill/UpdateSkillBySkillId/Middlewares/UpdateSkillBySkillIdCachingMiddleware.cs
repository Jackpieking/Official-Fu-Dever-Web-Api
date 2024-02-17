using Application.Interfaces.Caching;
using Domain.Specifications.Others.Interfaces;
using Domain.UnitOfWorks;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Skill.UpdateSkillBySkillId.Middlewares;

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

    public async Task<UpdateSkillBySkillIdResponse> Handle(
        UpdateSkillBySkillIdRequest request,
        RequestHandlerDelegate<UpdateSkillBySkillIdResponse> next,
        CancellationToken cancellationToken)
    {
        var response = await next();

        if (response.StatusCode == UpdateSkillBySkillIdStatusCode.OPERATION_SUCCESS)
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
                    key: $"{nameof(GetAllSkillsByName)}_param_{foundSkill.Name.ToLower()}",
                    cancellationToken: cancellationToken),
                _cacheHandler.RemoveAsync(
                    key: $"{nameof(GetAllSkillsByName)}_param_{request.NewSkillName.ToLower()}",
                    cancellationToken: cancellationToken),
                _cacheHandler.RemoveAsync(
                    key: nameof(GetAllSkills),
                    cancellationToken: cancellationToken));
        }

        return response;
    }
}
