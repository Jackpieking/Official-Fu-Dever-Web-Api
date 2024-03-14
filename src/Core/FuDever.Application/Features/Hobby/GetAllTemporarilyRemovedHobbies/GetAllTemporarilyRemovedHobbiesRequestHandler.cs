using FuDever.Domain.Specifications.Others.Interfaces;
using FuDever.Domain.UnitOfWorks;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Hobby.GetAllTemporarilyRemovedHobbies;

/// <summary>
///     Get all temporarily removed hobbies request handler.
/// </summary>
internal sealed class GetAllTemporarilyRemovedHobbiesRequestHandler : IRequestHandler<
    GetAllTemporarilyRemovedHobbiesRequest,
    GetAllTemporarilyRemovedHobbiesResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public GetAllTemporarilyRemovedHobbiesRequestHandler(
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
    public async Task<GetAllTemporarilyRemovedHobbiesResponse> Handle(
        GetAllTemporarilyRemovedHobbiesRequest request,
        CancellationToken cancellationToken)
    {
        // Get all temporarily removed hobbies.
        var foundTemporarilyRemovedHobbies = await GetAllTemporarilyRemovedHobbiesQueryAsync(cancellationToken: cancellationToken);

        // Project result to response.
        return new()
        {
            StatusCode = GetAllTemporarilyRemovedHobbiesResponseStatusCode.OPERATION_SUCCESS,
            FoundTemporarilyRemovedHobbies = foundTemporarilyRemovedHobbies.Select(selector: foundHobby =>
            {
                return new GetAllTemporarilyRemovedHobbiesResponse.Hobby()
                {
                    HobbyId = foundHobby.Id,
                    HobbyName = foundHobby.Name,
                    HobbyRemovedAt = foundHobby.RemovedAt,
                    HobbyRemovedBy = foundHobby.RemovedBy
                };
            })
        };
    }

    #region Queries
    /// <summary>
    ///     Get all hobbies which are temporarily removed.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     List of found hobbies.
    /// </returns>
    private Task<IEnumerable<Domain.Entities.Hobby>> GetAllTemporarilyRemovedHobbiesQueryAsync(CancellationToken cancellationToken)
    {
        return _unitOfWork.HobbyRepository.GetAllBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Hobby.HobbyAsNoTrackingSpecification,
                _superSpecificationManager.Hobby.HobbyTemporarilyRemovedSpecification,
                _superSpecificationManager.Hobby.SelectFieldsFromHobbySpecification.Ver2()
            ],
            cancellationToken: cancellationToken);
    }
    #endregion
}
