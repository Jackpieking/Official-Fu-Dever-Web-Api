using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Hobby.RestoreHobbyByHobbyId.Middlewares;

/// <summary>
///     Restore hobby by hobby id
///     request validation middleware.
/// </summary>
/// <remarks>
///     Order: 1st
/// </remarks>
internal sealed class RestoreHobbyByHobbyIdValidationMiddleware :
    IPipelineBehavior<
        RestoreHobbyByHobbyIdRequest,
        RestoreHobbyByHobbyIdResponse>,
    IRestoreHobbyByHobbyIdMiddleware
{
    private readonly IValidator<RestoreHobbyByHobbyIdRequest> _validator;

    public RestoreHobbyByHobbyIdValidationMiddleware(IValidator<RestoreHobbyByHobbyIdRequest> validator)
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
    ///     Response
    /// </returns>
    public async Task<RestoreHobbyByHobbyIdResponse> Handle(
        RestoreHobbyByHobbyIdRequest request,
        RequestHandlerDelegate<RestoreHobbyByHobbyIdResponse> next,
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
                StatusCode = RestoreHobbyByHobbyIdResponseStatusCode.INPUT_VALIDATION_FAIL,
            };
        }

        return await next();
    }
}
