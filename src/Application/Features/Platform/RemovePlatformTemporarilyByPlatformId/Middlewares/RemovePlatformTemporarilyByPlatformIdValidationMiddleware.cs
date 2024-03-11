using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Platform.RemovePlatformTemporarilyByPlatformId.Middlewares;

/// <summary>
///     Remove platform temporarily by platform
///     id request validation middleware.
/// </summary>
/// <remarks>
///     Order: 1st
/// </remarks>
internal sealed class RemovePlatformTemporarilyByPlatformIdValidationMiddleware :
    IPipelineBehavior<
        RemovePlatformTemporarilyByPlatformIdRequest,
        RemovePlatformTemporarilyByPlatformIdResponse>,
    IRemovePlatformTemporarilyByPlatformIdMiddleware
{
    private readonly IValidator<RemovePlatformTemporarilyByPlatformIdRequest> _validator;

    public RemovePlatformTemporarilyByPlatformIdValidationMiddleware(
        IValidator<RemovePlatformTemporarilyByPlatformIdRequest> validator)
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
    public async Task<RemovePlatformTemporarilyByPlatformIdResponse> Handle(
        RemovePlatformTemporarilyByPlatformIdRequest request,
        RequestHandlerDelegate<RemovePlatformTemporarilyByPlatformIdResponse> next,
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
                StatusCode = RemovePlatformTemporarilyByPlatformIdResponseStatusCode.INPUT_VALIDATION_FAIL
            };
        }

        return await next();
    }
}
