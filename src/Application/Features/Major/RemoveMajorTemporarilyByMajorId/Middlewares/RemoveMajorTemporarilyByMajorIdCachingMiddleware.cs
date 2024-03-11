using Application.Features.Major.GetAllMajors;
using Application.Features.Major.GetAllMajorsByMajorName;
using Application.Features.Major.GetAllTemporarilyRemovedMajors;
using Application.Interfaces.Caching;
using Domain.Specifications.Others.Interfaces;
using Domain.UnitOfWorks;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Major.RemoveMajorTemporarilyByMajorId.Middlewares;

/// <summary>
///     Remove major temporarily by major
///     id request caching middleware.
/// </summary>
/// <remarks>
///     Order: 2nd
/// </remarks>
internal sealed class RemoveMajorTemporarilyByMajorIdCachingMiddleware :
    IPipelineBehavior<
        RemoveMajorTemporarilyByMajorIdRequest,
        RemoveMajorTemporarilyByMajorIdResponse>,
    IRemoveMajorTemporarilyByMajorIdMiddleware
{
    private readonly ICacheHandler _cacheHandler;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public RemoveMajorTemporarilyByMajorIdCachingMiddleware(
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
    public async Task<RemoveMajorTemporarilyByMajorIdResponse> Handle(
        RemoveMajorTemporarilyByMajorIdRequest request,
        RequestHandlerDelegate<RemoveMajorTemporarilyByMajorIdResponse> next,
        CancellationToken cancellationToken)
    {
        var response = await next();

        if (response.StatusCode == RemoveMajorTemporarilyByMajorIdResponseStatusCode.OPERATION_SUCCESS)
        {
            var foundMajor = await _unitOfWork.MajorRepository.FindBySpecificationsAsync(
                specifications:
                [
                    _superSpecificationManager.Major.MajorByIdSpecification(majorId: request.MajorId),
                    _superSpecificationManager.Major.SelectFieldsFromMajorSpecification.Ver3()
                ],
                cancellationToken: cancellationToken);

            await Task.WhenAll(
                _cacheHandler.RemoveAsync(
                    key: $"{nameof(GetAllMajorsByMajorNameRequest)}_param_{foundMajor.Name.ToLower()}",
                    cancellationToken: cancellationToken),
                _cacheHandler.RemoveAsync(
                    key: nameof(GetAllMajorsRequest),
                    cancellationToken: cancellationToken),
                _cacheHandler.RemoveAsync(
                    key: nameof(GetAllTemporarilyRemovedMajorsRequest),
                    cancellationToken: cancellationToken));
        }

        return response;
    }
}
