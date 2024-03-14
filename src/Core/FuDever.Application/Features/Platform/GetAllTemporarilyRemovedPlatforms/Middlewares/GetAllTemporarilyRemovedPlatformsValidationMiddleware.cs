using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Platform.GetAllTemporarilyRemovedPlatforms.Middlewares;

/// <summary>
///     Get all temporarily removed platforms
///     request validation middleware.
/// </summary>
/// <remarks>
///     Order: 1st
/// </remarks>
internal sealed class GetAllTemporarilyRemovedPlatformsValidationMiddleware :
    IPipelineBehavior<
        GetAllTemporarilyRemovedPlatformsRequest,
        GetAllTemporarilyRemovedPlatformsResponse>,
    IGetAllTemporarilyRemovedPlatformsMiddleware
{
    private readonly IValidator<GetAllTemporarilyRemovedPlatformsRequest> _validator;

    public GetAllTemporarilyRemovedPlatformsValidationMiddleware(IValidator<GetAllTemporarilyRemovedPlatformsRequest> validator)
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
    public async Task<GetAllTemporarilyRemovedPlatformsResponse> Handle(
        GetAllTemporarilyRemovedPlatformsRequest request,
        RequestHandlerDelegate<GetAllTemporarilyRemovedPlatformsResponse> next,
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
                StatusCode = GetAllTemporarilyRemovedPlatformsResponseStatusCode.INPUT_VALIDATION_FAIL,
                FoundTemporarilyRemovedPlatforms = default
            };
        }

        return await next();
    }
}
