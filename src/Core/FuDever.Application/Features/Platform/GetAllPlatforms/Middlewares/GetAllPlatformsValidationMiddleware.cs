using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Platform.GetAllPlatforms.Middlewares;

/// <summary>
///     Get all platforms request validation middleware.
/// </summary>
/// <remarks>
///     Order: 1st
/// </remarks>
internal sealed class GetAllPlatformsValidationMiddleware :
    IPipelineBehavior<
        GetAllPlatformsRequest,
        GetAllPlatformsResponse>,
    IGetAllPlatformsMiddleware
{
    private readonly IValidator<GetAllPlatformsRequest> _validator;

    public GetAllPlatformsValidationMiddleware(IValidator<GetAllPlatformsRequest> validator)
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
    public async Task<GetAllPlatformsResponse> Handle(
        GetAllPlatformsRequest request,
        RequestHandlerDelegate<GetAllPlatformsResponse> next,
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
                StatusCode = GetAllPlatformsResponseStatusCode.INPUT_VALIDATION_FAIL,
                FoundPlatforms = default
            };
        }

        return await next();
    }
}
