using MediatR;
using System.Threading.Tasks;
using System.Threading;
using FluentValidation;

namespace Application.Features.Platform.RemovePlatformPermanentlyByPlatformId.Middlewares;

/// <summary>
///     Remove platform permanently by platform id
///     request validation middleware.
/// </summary>
/// <remarks>
///     Order: 1st
/// </remarks>
internal sealed class RemovePlatformPermanentlyByPlatformIdValidationMiddleware :
    IPipelineBehavior<
        RemovePlatformPermanentlyByPlatformIdRequest,
        RemovePlatformPermanentlyByPlatformIdResponse>,
    IRemovePlatformPermanentlyByPlatformIdMiddleware
{
    private readonly IValidator<RemovePlatformPermanentlyByPlatformIdRequest> _validator;

    public RemovePlatformPermanentlyByPlatformIdValidationMiddleware(IValidator<RemovePlatformPermanentlyByPlatformIdRequest> validator)
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
    public async Task<RemovePlatformPermanentlyByPlatformIdResponse> Handle(
        RemovePlatformPermanentlyByPlatformIdRequest request,
        RequestHandlerDelegate<RemovePlatformPermanentlyByPlatformIdResponse> next,
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
                StatusCode = RemovePlatformPermanentlyByPlatformIdResponseStatusCode.INPUT_VALIDATION_FAIL
            };
        }

        return await next();
    }
}
