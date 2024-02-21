using Application.Features.Department.UpdateDepartmentByDepartmentId.Middlewares;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Position.UpdatePosition.Middlewares;

/// <summary>
///     Update position by position id
///     request validation middleware.
/// </summary>
/// <remarks>
///     Order: 1st
/// </remarks>
internal sealed class UpdatePositionByPositionIdValidationMiddleware :
    IPipelineBehavior<
        UpdatePositionByPositionIdRequest,
        UpdatePositionByPositionIdResponse>,
    IUpdateDepartmentByDepartmentIdMiddleware
{
    private readonly IValidator<UpdatePositionByPositionIdRequest> _validator;

    public UpdatePositionByPositionIdValidationMiddleware(IValidator<UpdatePositionByPositionIdRequest> validator)
    {
        _validator = validator;
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
        // Validate input.
        var inputValidationResult = await _validator.ValidateAsync(
            instance: request,
            cancellation: cancellationToken);

        // Input is not valid.
        if (!inputValidationResult.IsValid)
        {
            return new()
            {
                StatusCode = UpdatePositionByPositionIdStatusCode.INPUT_VALIDATION_FAIL,
            };
        }

        return await next();
    }
}
