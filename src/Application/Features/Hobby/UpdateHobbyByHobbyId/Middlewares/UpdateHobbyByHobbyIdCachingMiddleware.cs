using Application.Features.Hobby.GetAllHobbies;
using Application.Features.Hobby.GetAllHobbiesByHobbyName;
using Application.Interfaces.Caching;
using Domain.Specifications.Others.Interfaces;
using Domain.UnitOfWorks;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Hobby.UpdateHobbyByHobbyId.Middlewares;

/// <summary>
///     Update hobby by hobby id
///     request caching middleware.
/// </summary>
/// <remarks>
///     Order: 2nd
/// </remarks>
internal sealed class UpdateHobbyByHobbyIdCachingMiddleware :
    IPipelineBehavior<
        UpdateHobbyByHobbyIdRequest,
        UpdateHobbyByHobbyIdResponse>,
    IUpdateHobbyByHobbyIdMiddleware
{
    private readonly ICacheHandler _cacheHandler;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public UpdateHobbyByHobbyIdCachingMiddleware(
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
    public async Task<UpdateHobbyByHobbyIdResponse> Handle(
        UpdateHobbyByHobbyIdRequest request,
        RequestHandlerDelegate<UpdateHobbyByHobbyIdResponse> next,
        CancellationToken cancellationToken)
    {
        // Finding current hobby by hobby id.
        var foundHobby = await _unitOfWork.HobbyRepository.FindBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Hobby.HobbyByIdSpecification(hobbyId: request.HobbyId),
                _superSpecificationManager.Hobby.SelectFieldsFromHobbySpecification.Ver4()
            ],
            cancellationToken: cancellationToken);

        // Hobby is found by hobby id.
        if (!Equals(objA: foundHobby, objB: default))
        {
            await _cacheHandler.RemoveAsync(
                key: $"{nameof(GetAllHobbiesByHobbyNameRequest)}_param_{foundHobby.Name.ToLower()}",
                cancellationToken: cancellationToken);
        }

        var response = await next();

        if (response.StatusCode == UpdateHobbyByHobbyIdResponseStatusCode.OPERATION_SUCCESS)
        {
            await Task.WhenAll(
                _cacheHandler.RemoveAsync(
                    key: $"{nameof(GetAllHobbiesByHobbyNameRequest)}_param_{request.NewHobbyName.ToLower()}",
                    cancellationToken: cancellationToken),
                _cacheHandler.RemoveAsync(
                    key: nameof(GetAllHobbiesRequest),
                    cancellationToken: cancellationToken));
        }

        return response;
    }
}
