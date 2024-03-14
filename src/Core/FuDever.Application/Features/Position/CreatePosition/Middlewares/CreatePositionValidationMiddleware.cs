using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Position.CreatePosition.Middlewares;

/// <summary>
///     Create position request caching middleware.
/// </summary>
/// <remarks>
///     Order: 1st
/// </remarks>
internal sealed class CreatePositionValidationMiddleware :
    IPipelineBehavior<
        CreatePositionRequest,
        CreatePositionResponse>,
    ICreatePositionMiddleware
{
    private readonly IValidator<CreatePositionRequest> _validator;

    public CreatePositionValidationMiddleware(IValidator<CreatePositionRequest> validator)
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
    public async Task<CreatePositionResponse> Handle(
        CreatePositionRequest request,
        RequestHandlerDelegate<CreatePositionResponse> next,
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
                StatusCode = CreatePositionResponseStatusCode.INPUT_VALIDATION_FAIL
            };
        }

        return await next();
    }
}
