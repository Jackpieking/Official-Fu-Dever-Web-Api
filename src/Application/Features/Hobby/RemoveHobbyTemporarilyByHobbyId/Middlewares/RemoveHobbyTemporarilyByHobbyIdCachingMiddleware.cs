using System.Threading;
using System.Threading.Tasks;
using Application.Features.Hobby.GetAllHobbies;
using Application.Features.Hobby.GetAllHobbiesByHobbyName;
using Application.Features.Hobby.GetAllTemporarilyRemovedHobbies;
using Application.Interfaces.Caching;
using Domain.Specifications.Others.Interfaces;
using Domain.UnitOfWorks;
using MediatR;

namespace Application.Features.Hobby.RemoveHobbyTemporarilyByHobbyId.Middlewares;

/// <summary>
///     Remove hobby temporarily by 
///     hobby id caching middleware.
/// </summary>
/// <remarks>
///     Order: 2nd
/// </remarks>
internal sealed class RemoveHobbyTemporarilyByHobbyIdCachingMiddleware :
    IPipelineBehavior<
        RemoveHobbyTemporarilyByHobbyIdRequest,
        RemoveHobbyTemporarilyByHobbyIdResponse>,
    IRemoveHobbyTemporarilyByHobbyIdMiddleware
{
    private readonly ICacheHandler _cacheHandler;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public RemoveHobbyTemporarilyByHobbyIdCachingMiddleware(
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
    public async Task<RemoveHobbyTemporarilyByHobbyIdResponse> Handle(
        RemoveHobbyTemporarilyByHobbyIdRequest request,
        RequestHandlerDelegate<RemoveHobbyTemporarilyByHobbyIdResponse> next,
        CancellationToken cancellationToken)
    {
        var response = await next();

        if (response.StatusCode == RemoveHobbyTemporarilyByHobbyIdResponseStatusCode.OPERATION_SUCCESS)
        {
            var foundHobby = await _unitOfWork.HobbyRepository.FindBySpecificationsAsync(
                specifications:
                [
                    _superSpecificationManager.Hobby.HobbyByIdSpecification(hobbyId: request.HobbyId),
                    _superSpecificationManager.Hobby.SelectFieldsFromHobbySpecification.Ver4()
                ],
                cancellationToken: cancellationToken);

            await Task.WhenAll(
                _cacheHandler.RemoveAsync(
                    key: $"{nameof(GetAllHobbiesByHobbyNameRequest)}_param_{foundHobby.Name.ToLower()}",
                    cancellationToken: cancellationToken),
                _cacheHandler.RemoveAsync(
                    key: nameof(GetAllHobbiesRequest),
                    cancellationToken: cancellationToken),
                _cacheHandler.RemoveAsync(
                    key: nameof(GetAllTemporarilyRemovedHobbiesRequest),
                    cancellationToken: cancellationToken));
        }

        return response;
    }
}
