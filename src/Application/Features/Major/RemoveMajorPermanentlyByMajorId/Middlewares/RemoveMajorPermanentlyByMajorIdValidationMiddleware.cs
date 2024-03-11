using MediatR;
using System.Threading.Tasks;
using System.Threading;
using FluentValidation;

namespace Application.Features.Major.RemoveMajorPermanentlyByMajorId.Middlewares;

/// <summary>
///     Remove permanently major by
///     major id request validation middleware.
/// </summary>
/// <remarks>
///     Order: 1st
/// </remarks>
internal sealed class RemoveMajorPermanentlyByMajorIdValidationMiddleware :
    IPipelineBehavior<
        RemoveMajorPermanentlyByMajorIdRequest,
        RemoveMajorPermanentlyByMajorIdResponse>,
    IRemoveMajorPermanentlyByMajorIdMiddleware
{
    private readonly IValidator<RemoveMajorPermanentlyByMajorIdRequest> _validator;

    public RemoveMajorPermanentlyByMajorIdValidationMiddleware(IValidator<RemoveMajorPermanentlyByMajorIdRequest> validator)
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
    public async Task<RemoveMajorPermanentlyByMajorIdResponse> Handle(
        RemoveMajorPermanentlyByMajorIdRequest request,
        RequestHandlerDelegate<RemoveMajorPermanentlyByMajorIdResponse> next,
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
                StatusCode = RemoveMajorPermanentlyByMajorIdResponseStatusCode.INPUT_VALIDATION_FAIL,
            };
        }

        return await next();
    }
}
