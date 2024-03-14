using FuDever.Application.Features.Hobby.GetAllHobbies;
using FuDever.Application.Features.Hobby.GetAllHobbiesByHobbyName;
using FuDever.Application.Features.Hobby.GetAllTemporarilyRemovedHobbies;
using FuDever.Application.Interfaces.Caching;
using FuDever.Domain.Specifications.Others.Interfaces;
using FuDever.Domain.UnitOfWorks;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Hobby.RestoreHobbyByHobbyId.Middlewares;

/// <summary>
///     Restore hobby by hobby id
///     request caching middleware.
/// </summary>
/// <remarks>
///     Order: 2nd
/// </remarks>
internal sealed class RestoreHobbyByHobbyIdCachingMiddleware :
    IPipelineBehavior<
        RestoreHobbyByHobbyIdRequest,
        RestoreHobbyByHobbyIdResponse>,
    IRestoreHobbyByHobbyIdMiddleware
{
    private readonly ICacheHandler _cacheHandler;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public RestoreHobbyByHobbyIdCachingMiddleware(
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
    public async Task<RestoreHobbyByHobbyIdResponse> Handle(
        RestoreHobbyByHobbyIdRequest request,
        RequestHandlerDelegate<RestoreHobbyByHobbyIdResponse> next,
        CancellationToken cancellationToken)
    {
        var response = await next();

        if (response.StatusCode == RestoreHobbyByHobbyIdResponseStatusCode.OPERATION_SUCCESS)
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
