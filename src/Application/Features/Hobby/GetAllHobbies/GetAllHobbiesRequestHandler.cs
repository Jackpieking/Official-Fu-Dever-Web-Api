using Domain.Specifications.Others.Interfaces;
using Domain.UnitOfWorks;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Hobby.GetAllHobbies;

/// <summary>
///     Get all hobbies request handler.
/// </summary>
internal sealed class GetAllHobbiesRequestHandler : IRequestHandler<
    GetAllHobbiesRequest,
    GetAllHobbiesResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public GetAllHobbiesRequestHandler(
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
    public async Task<GetAllHobbiesResponse> Handle(
        GetAllHobbiesRequest request,
        CancellationToken cancellationToken)
    {
        // Get all non temporarily removed hobbies.
        var foundHobbies = await GetAllNonTemporarilyRemovedHobbiesQueryAsync(cancellationToken: cancellationToken);

        // Project result to response.
        return new()
        {
            StatusCode = GetAllHobbiesResponseStatusCode.OPERATION_SUCCESS,
            FoundHobbies = foundHobbies.Select(selector: foundHobby =>
            {
                return new GetAllHobbiesResponse.Hobby()
                {
                    HobbyId = foundHobby.Id,
                    HobbyName = foundHobby.Name
                };
            })
        };
    }

    #region Queries
    /// <summary>
    ///     Get all non temporarily removed hobbies query.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     A task containing the response.
    /// </returns>
    private Task<IEnumerable<Domain.Entities.Hobby>> GetAllNonTemporarilyRemovedHobbiesQueryAsync(CancellationToken cancellationToken)
    {
        return _unitOfWork.HobbyRepository.GetAllBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Hobby.HobbyAsNoTrackingSpecification,
                _superSpecificationManager.Hobby.HobbyNotTemporarilyRemovedSpecification,
                _superSpecificationManager.Hobby.SelectFieldsFromHobbySpecification.Ver1()
            ],
            cancellationToken: cancellationToken);
    }
    #endregion
}
