using FuDever.Domain.Specifications.Others.Interfaces;
using FuDever.Domain.UnitOfWorks;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Major.GetAllTemporarilyRemovedMajors;

/// <summary>
///     Get all temporarily removed majors request handler.
/// </summary>
internal sealed class GetAllTemporarilyRemovedMajorsRequestHandler : IRequestHandler<
    GetAllTemporarilyRemovedMajorsRequest,
    GetAllTemporarilyRemovedMajorsResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public GetAllTemporarilyRemovedMajorsRequestHandler(
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
    public async Task<GetAllTemporarilyRemovedMajorsResponse> Handle(
        GetAllTemporarilyRemovedMajorsRequest request,
        CancellationToken cancellationToken)
    {
        // Get all temporarily removed majors.
        var foundTemporarilyRemovedMajors = await GetAllTemporarilyRemovedMajorsQueryAsync(cancellationToken: cancellationToken);

        // Project result to response.
        return new()
        {
            StatusCode = GetAllTemporarilyRemovedMajorsResponseStatusCode.OPERATION_SUCCESS,
            FoundTemporarilyRemovedMajors = foundTemporarilyRemovedMajors.Select(selector: foundMajor =>
            {
                return new GetAllTemporarilyRemovedMajorsResponse.Major()
                {
                    MajorId = foundMajor.Id,
                    MajorName = foundMajor.Name,
                    MajorRemovedAt = foundMajor.RemovedAt,
                    MajorRemovedBy = foundMajor.RemovedBy
                };
            })
        };
    }

    #region Queries
    /// <summary>
    ///     Get all majors which are temporarily removed.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     List of found majors.
    /// </returns>
    private Task<IEnumerable<Domain.Entities.Major>> GetAllTemporarilyRemovedMajorsQueryAsync(CancellationToken cancellationToken)
    {
        return _unitOfWork.MajorRepository.GetAllBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Major.MajorAsNoTrackingSpecification,
                _superSpecificationManager.Major.MajorTemporarilyRemovedSpecification,
                _superSpecificationManager.Major.SelectFieldsFromMajorSpecification.Ver2()
            ],
            cancellationToken: cancellationToken);
    }
    #endregion
}
