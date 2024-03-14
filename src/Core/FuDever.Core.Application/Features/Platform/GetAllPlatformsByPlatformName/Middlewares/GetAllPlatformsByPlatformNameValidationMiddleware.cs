using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Platform.GetAllPlatformsByPlatformName.Middlewares;

/// <summary>
///     Get all platforms by platform name
///     request validation middleware.
/// </summary>
/// <remarks>
///     Order: 1st
/// </remarks>
internal sealed class GetAllPlatformsByPlatformNameValidationMiddleware :
    IPipelineBehavior<
        GetAllPlatformsByPlatformNameRequest,
        GetAllPlatformsByPlatformNameResponse>,
    IGetAllPlatformsByPlatformNameMiddleware
{
    private readonly IValidator<GetAllPlatformsByPlatformNameRequest> _validator;

    public GetAllPlatformsByPlatformNameValidationMiddleware(IValidator<GetAllPlatformsByPlatformNameRequest> validator)
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
    public async Task<GetAllPlatformsByPlatformNameResponse> Handle(
        GetAllPlatformsByPlatformNameRequest request,
        RequestHandlerDelegate<GetAllPlatformsByPlatformNameResponse> next,
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
                StatusCode = GetAllPlatformsByPlatformNameResponseStatusCode.INPUT_VALIDATION_FAIL,
                FoundPlatforms = default
            };
        }

        return await next();
    }
}
