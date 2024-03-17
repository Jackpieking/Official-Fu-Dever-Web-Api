using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Role.RemoveRoleTemporarilyByRoleId.Middlewares;

/// <summary>
///     Remove role temporarily by role
///     id request validation middleware.
/// </summary>
/// <remarks>
///     Order: 1st
/// </remarks>
internal sealed class RemoveRoleTemporarilyByRoleIdValidationMiddleware :
    IPipelineBehavior<
        RemoveRoleTemporarilyByRoleIdRequest,
        RemoveRoleTemporarilyByRoleIdResponse>,
    IRemoveRoleTemporarilyByRoleIdMiddleware
{
    private readonly IValidator<RemoveRoleTemporarilyByRoleIdRequest> _validator;

    public RemoveRoleTemporarilyByRoleIdValidationMiddleware(IValidator<RemoveRoleTemporarilyByRoleIdRequest> validator)
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
    public async Task<RemoveRoleTemporarilyByRoleIdResponse> Handle(
        RemoveRoleTemporarilyByRoleIdRequest request,
        RequestHandlerDelegate<RemoveRoleTemporarilyByRoleIdResponse> next,
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
                StatusCode = RemoveRoleTemporarilyByRoleIdResponseStatusCode.INPUT_VALIDATION_FAIL
            };
        }

        return await next();
    }
}
