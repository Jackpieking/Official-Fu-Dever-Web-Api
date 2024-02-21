using Application.Features.Department.UpdateDepartmentByDepartmentId.Middlewares;
using Application.Features.Position.GetAllPositions;
using Application.Features.Position.GetAllPositionsByPositionName;
using Application.Interfaces.Caching;
using Domain.Specifications.Others.Interfaces;
using Domain.UnitOfWorks;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Position.UpdatePosition.Middlewares;

/// <summary>
///     Update position by position id
///     request caching middleware.
/// </summary>
/// <remarks>
///     Order: 2nd
/// </remarks>
internal sealed class UpdatePositionByPositionIdCachingMiddleware :
    IPipelineBehavior<
        UpdatePositionByPositionIdRequest,
        UpdatePositionByPositionIdResponse>,
    IUpdateDepartmentByDepartmentIdMiddleware
{
    private readonly ICacheHandler _cacheHandler;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISuperSpecificationManager _superSpecificationManager;

    public UpdatePositionByPositionIdCachingMiddleware(
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

    public async Task<UpdatePositionByPositionIdResponse> Handle(
        UpdatePositionByPositionIdRequest request,
        RequestHandlerDelegate<UpdatePositionByPositionIdResponse> next,
        CancellationToken cancellationToken)
    {
        // finding current position by position id.
        var foundPosition = await _unitOfWork.PositionRepository.FindBySpecificationsAsync(
            specifications:
            [
                _superSpecificationManager.Position.PositionByIdSpecification(positionId: request.PositionId),
                _superSpecificationManager.Position.SelectFieldsFromPositionSpecification.Ver3()
            ],
            cancellationToken: cancellationToken);

        // Position is found by position id.
        if (!Equals(objA: foundPosition, objB: default))
        {
            await _cacheHandler.RemoveAsync(
                key: $"{nameof(GetAllPositionsByPositionNameRequest)}_param_{foundPosition.Name.ToLower()}",
                cancellationToken: cancellationToken);
        }

        var response = await next();

        if (response.StatusCode == UpdatePositionByPositionIdStatusCode.OPERATION_SUCCESS)
        {
            await Task.WhenAll(
                _cacheHandler.RemoveAsync(
                    key: $"{nameof(GetAllPositionsByPositionNameRequest)}_param_{request.NewPositionName.ToLower()}",
                    cancellationToken: cancellationToken),
                _cacheHandler.RemoveAsync(
                    key: nameof(GetAllPositionsRequest),
                    cancellationToken: cancellationToken));
        }

        return response;
    }
}
