using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Hobby.UpdateHobbyByHobbyId.Middlewares;

/// <summary>
///     Update hobby by hobby id
///     request validation middleware.
/// </summary>
/// <remarks>
///     Order: 1st
/// </remarks>
internal sealed class UpdateHobbyByHobbyIdValidationMiddleware :
    IPipelineBehavior<
        UpdateHobbyByHobbyIdRequest,
        UpdateHobbyByHobbyIdResponse>,
    IUpdateHobbyByHobbyIdMiddleware
{
    private readonly IValidator<UpdateHobbyByHobbyIdRequest> _validator;

    public UpdateHobbyByHobbyIdValidationMiddleware(IValidator<UpdateHobbyByHobbyIdRequest> validator)
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
    public async Task<UpdateHobbyByHobbyIdResponse> Handle(
        UpdateHobbyByHobbyIdRequest request,
        RequestHandlerDelegate<UpdateHobbyByHobbyIdResponse> next,
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
                StatusCode = UpdateHobbyByHobbyIdResponseStatusCode.INPUT_VALIDATION_FAIL,
            };
        }

        return await next();
    }
}
