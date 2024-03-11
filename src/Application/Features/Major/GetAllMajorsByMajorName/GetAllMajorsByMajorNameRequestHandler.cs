﻿using Domain.Specifications.Others.Interfaces;
using Domain.UnitOfWorks;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Major.GetAllMajorsByMajorName;

/// <summary>
///     Get all majors by major name request handler.
/// </summary>
internal sealed class GetAllMajorsByMajorNameRequestHandler : IRequestHandler<
    GetAllMajorsByMajorNameRequest,
    GetAllMajorsByMajorNameResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public GetAllMajorsByMajorNameRequestHandler(
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
    public async Task<GetAllMajorsByMajorNameResponse> Handle(
        GetAllMajorsByMajorNameRequest request,
        CancellationToken cancellationToken)
    {
        // Get all majors by major name.
        var foundMajors = await GetAllMajorsByMajorNameQueryAsync(
            majorName: request.MajorName,
            cancellationToken: cancellationToken);

        // Project result to response.
        return new()
        {
            StatusCode = GetAllMajorsByMajorNameResponseStatusCode.OPERATION_SUCCESS,
            FoundMajors = foundMajors.Select(selector: foundMajor =>
            {
                return new GetAllMajorsByMajorNameResponse.Major()
                {
                    MajorId = foundMajor.Id,
                    MajorName = foundMajor.Name
                };
            })
        };
    }

    #region Queries
    /// <summary>
    ///     Retrieves all majors based on
    ///     the major name asynchronously.
    /// </summary>
    /// <param name="majorName">
    ///     The name of the major to search for.
    /// </param>
    /// <param name="cancellationToken">
    ///     The cancellation token.
    /// </param>
    /// <returns>
    ///     A task representing the asynchronous operation
    ///     that yields an enumerable collection of majors.
    /// </returns>
    private Task<IEnumerable<Domain.Entities.Major>> GetAllMajorsByMajorNameQueryAsync(
        string majorName,
        CancellationToken cancellationToken)
    {
        return _unitOfWork.MajorRepository.GetAllBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Major.MajorAsNoTrackingSpecification,
                _superSpecificationManager.Major.MajorByNameSpecification(
                    majorName: majorName,
                    isCaseSensitive: false),
                _superSpecificationManager.Major.MajorNotTemporarilyRemovedSpecification,
                _superSpecificationManager.Major.SelectFieldsFromMajorSpecification.Ver1()
            ],
            cancellationToken: cancellationToken);
    }
    #endregion
}
