using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Position.GetAllPositionsByPositionName.Middlewares;

/// <summary>
///     Get all positions by position name
///     request validation middleware.
/// </summary>
/// <remarks>
///     Order: 1st
/// </remarks>
internal sealed class GetAllPositionsByPositionNameValidationMiddleware :
    IPipelineBehavior<
        GetAllPositionsByPositionNameRequest,
        GetAllPositionsByPositionNameResponse>,
    IGetAllPositionsByPositionNameMiddleware
{
    private readonly IValidator<GetAllPositionsByPositionNameRequest> _validator;

    public GetAllPositionsByPositionNameValidationMiddleware(IValidator<GetAllPositionsByPositionNameRequest> validator)
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
    public async Task<GetAllPositionsByPositionNameResponse> Handle(
        GetAllPositionsByPositionNameRequest request,
        RequestHandlerDelegate<GetAllPositionsByPositionNameResponse> next,
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
                StatusCode = GetAllPositionsByPositionNameResponseStatusCode.INPUT_VALIDATION_FAIL,
                FoundPositions = default
            };
        }

        return await next();
    }
}
