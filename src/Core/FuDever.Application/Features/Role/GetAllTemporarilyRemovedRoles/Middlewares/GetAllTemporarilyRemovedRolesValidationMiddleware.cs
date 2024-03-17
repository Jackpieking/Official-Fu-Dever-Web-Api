using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Role.GetAllTemporarilyRemovedRoles.Middlewares;

/// <summary>
///     Get all temporarily removed roles
///     request validation middleware.
/// </summary>
/// <remarks>
///     Order: 1st
/// </remarks>
internal sealed class GetAllTemporarilyRemovedRolesValidationMiddleware :
    IPipelineBehavior<
        GetAllTemporarilyRemovedRolesRequest,
        GetAllTemporarilyRemovedRolesResponse>,
    IGetAllTemporarilyRemovedRolesMiddleware
{
    private readonly IValidator<GetAllTemporarilyRemovedRolesRequest> _validator;

    public GetAllTemporarilyRemovedRolesValidationMiddleware(IValidator<GetAllTemporarilyRemovedRolesRequest> validator)
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
    public async Task<GetAllTemporarilyRemovedRolesResponse> Handle(
        GetAllTemporarilyRemovedRolesRequest request,
        RequestHandlerDelegate<GetAllTemporarilyRemovedRolesResponse> next,
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
                StatusCode = GetAllTemporarilyRemovedRolesResponseStatusCode.INPUT_VALIDATION_FAIL,
                FoundTemporarilyRemovedRoles = default
            };
        }

        return await next();
    }
}
