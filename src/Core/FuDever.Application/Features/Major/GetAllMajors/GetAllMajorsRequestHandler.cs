using FuDever.Domain.Specifications.Others.Interfaces;
using FuDever.Domain.UnitOfWorks;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Major.GetAllMajors;

/// <summary>
///     Get all majors request handler.
/// </summary>
internal sealed class GetAllMajorsRequestHandler : IRequestHandler<
    GetAllMajorsRequest,
    GetAllMajorsResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public GetAllMajorsRequestHandler(
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
    public async Task<GetAllMajorsResponse> Handle(
        GetAllMajorsRequest request,
        CancellationToken cancellationToken)
    {
        // Get all non temporarily removed majors.
        var foundMajors = await GetAllNonTemporarilyRemovedMajorsQueryAsync(cancellationToken: cancellationToken);

        // Project result to response.
        return new()
        {
            StatusCode = GetAllMajorsResponseStatusCode.OPERATION_SUCCESS,
            FoundMajors = foundMajors.Select(selector: foundMajor =>
            {
                return new GetAllMajorsResponse.Major()
                {
                    MajorId = foundMajor.Id,
                    MajorName = foundMajor.Name
                };
            })
        };
    }

    #region Queries
    /// <summary>
    ///     Get all majors which are not temporarily removed.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     List of found majors.
    /// </returns>
    private Task<IEnumerable<Domain.Entities.Major>> GetAllNonTemporarilyRemovedMajorsQueryAsync(CancellationToken cancellationToken)
    {
        return _unitOfWork.MajorRepository.GetAllBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Major.MajorAsNoTrackingSpecification,
                _superSpecificationManager.Major.MajorNotTemporarilyRemovedSpecification,
                _superSpecificationManager.Major.SelectFieldsFromMajorSpecification.Ver1()
            ],
            cancellationToken: cancellationToken);
    }
    #endregion
}
