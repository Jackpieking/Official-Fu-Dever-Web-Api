using FuDever.Domain.Specifications.Others.Interfaces;
using FuDever.Domain.UnitOfWorks;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Hobby.GetAllHobbiesByHobbyName;

/// <summary>
///     Request handler for getting all hobbies by hobby name.
/// </summary>
internal sealed class GetAllHobbiesByHobbyNameRequestHandler : IRequestHandler<
    GetAllHobbiesByHobbyNameRequest,
    GetAllHobbiesByHobbyNameResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public GetAllHobbiesByHobbyNameRequestHandler(
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
    public async Task<GetAllHobbiesByHobbyNameResponse> Handle(
        GetAllHobbiesByHobbyNameRequest request,
        CancellationToken cancellationToken)
    {
        // Get all hobbies by name.
        var foundHobbies = await GetAllHobbiesByHobbyNameQueryAsync(
            hobbyName: request.HobbyName,
            cancellationToken: cancellationToken);

        return new()
        {
            StatusCode = GetAllHobbiesByHobbyNameResponseStatusCode.OPERATION_SUCCESS,
            FoundHobbies = foundHobbies.Select(selector: foundHobby =>
            {
                return new GetAllHobbiesByHobbyNameResponse.Hobby()
                {
                    HobbyId = foundHobby.Id,
                    HobbyName = foundHobby.Name
                };
            }),
        };
    }

    #region Queries
    /// <summary>
    ///     Get all hobby by hobby name
    /// </summary>
    /// <param name="hobbyName">
    ///     Hobby name to find.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     List of found hobbies.
    /// </returns>
    private Task<IEnumerable<Domain.Entities.Hobby>> GetAllHobbiesByHobbyNameQueryAsync(
        string hobbyName,
        CancellationToken cancellationToken)
    {
        return _unitOfWork.HobbyRepository.GetAllBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Hobby.HobbyAsNoTrackingSpecification,
                _superSpecificationManager.Hobby.HobbyByNameSpecification(
                        hobbyName: hobbyName,
                        isCaseSensitive: false),
                _superSpecificationManager.Hobby.HobbyNotTemporarilyRemovedSpecification,
                _superSpecificationManager.Hobby.SelectFieldsFromHobbySpecification.Ver1()
            ],
            cancellationToken: cancellationToken);
    }
    #endregion
}
