using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Position.GetAllTemporarilyRemovedPositions.Middlewares;

/// <summary>
///     Get all temporarily removed positions
///     request validation middleware.
/// </summary>
/// <remarks>
///     Order: 1st
/// </remarks>
internal sealed class GetAllTemporarilyRemovedPositionsValidationMiddleware :
    IPipelineBehavior<
        GetAllTemporarilyRemovedPositionsRequest,
        GetAllTemporarilyRemovedPositionsResponse>,
    IGetAllTemporarilyRemovedPositionsMiddleware
{
    private readonly IValidator<GetAllTemporarilyRemovedPositionsRequest> _validator;

    public GetAllTemporarilyRemovedPositionsValidationMiddleware(IValidator<GetAllTemporarilyRemovedPositionsRequest> validator)
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

    public async Task<GetAllTemporarilyRemovedPositionsResponse> Handle(
        GetAllTemporarilyRemovedPositionsRequest request,
        RequestHandlerDelegate<GetAllTemporarilyRemovedPositionsResponse> next,
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
                StatusCode = GetAllTemporarilyRemovedPositionsStatusCode.INPUT_VALIDATION_FAIL,
                FoundTemporarilyRemovedPositions = default
            };
        }

        return await next();
    }
}

