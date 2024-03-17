using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Role.UpdateRoleByRoleId.Middlewares;

/// <summary>
///     Update role by role id
///     request validation middleware.
/// </summary>
/// <remarks>
///     Order: 1st
/// </remarks>
internal sealed class UpdateRoleByRoleIdValidationMiddleware :
    IPipelineBehavior<
        UpdateRoleByRoleIdRequest,
        UpdateRoleByRoleIdResponse>,
    IUpdateRoleByRoleIdMiddleware
{
    private readonly IValidator<UpdateRoleByRoleIdRequest> _validator;

    public UpdateRoleByRoleIdValidationMiddleware(IValidator<UpdateRoleByRoleIdRequest> validator)
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
    public async Task<UpdateRoleByRoleIdResponse> Handle(
        UpdateRoleByRoleIdRequest request,
        RequestHandlerDelegate<UpdateRoleByRoleIdResponse> next,
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
                StatusCode = UpdateRoleByRoleIdResponseStatusCode.INPUT_VALIDATION_FAIL,
            };
        }

        return await next();
    }
}
