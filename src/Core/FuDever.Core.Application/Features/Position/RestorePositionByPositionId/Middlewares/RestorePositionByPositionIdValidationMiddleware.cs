using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Position.RestorePositionByPositionId.Middlewares;

/// <summary>
///     Restore position by position
///     id request validation middleware.
/// </summary>
/// <remarks>
///     Order: 1st
/// </remarks>
internal sealed class RestorePositionByPositionIdValidationMiddleware :
    IPipelineBehavior<
        RestorePositionByPositionIdRequest,
        RestorePositionByPositionIdResponse>,
    IRestorePositionByPositionIdMiddleware
{
    private readonly IValidator<RestorePositionByPositionIdRequest> _validator;

    public RestorePositionByPositionIdValidationMiddleware(
        IValidator<RestorePositionByPositionIdRequest> validator)
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
    public async Task<RestorePositionByPositionIdResponse> Handle(
        RestorePositionByPositionIdRequest request,
        RequestHandlerDelegate<RestorePositionByPositionIdResponse> next,
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
                StatusCode = RestorePositionByPositionIdResponseStatusCode.INPUT_VALIDATION_FAIL
            };
        }

        return await next();
    }
}
