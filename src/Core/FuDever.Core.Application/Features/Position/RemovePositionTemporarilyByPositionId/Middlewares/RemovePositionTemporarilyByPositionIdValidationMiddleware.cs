using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Position.RemovePositionTemporarilyByPositionId.Middlewares;

/// <summary>
///     Remove position temporarily by position
///     id request validation middleware.
/// </summary>
/// <remarks>
///     Order: 1st
/// </remarks>
internal sealed class RemovePositionTemporarilyByPositionIdValidationMiddleware :
    IPipelineBehavior<
        RemovePositionTemporarilyByPositionIdRequest,
        RemovePositionTemporarilyByPositionIdResponse>,
    IRemovePositionTemporarilyByPositionIdMiddleware
{
    private readonly IValidator<RemovePositionTemporarilyByPositionIdRequest> _validator;

    public RemovePositionTemporarilyByPositionIdValidationMiddleware(
        IValidator<RemovePositionTemporarilyByPositionIdRequest> validator)
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
    public async Task<RemovePositionTemporarilyByPositionIdResponse> Handle(
        RemovePositionTemporarilyByPositionIdRequest request,
        RequestHandlerDelegate<RemovePositionTemporarilyByPositionIdResponse> next,
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
                StatusCode = RemovePositionTemporarilyByPositionIdResponseStatusCode.INPUT_VALIDATION_FAIL
            };
        }

        return await next();
    }
}
