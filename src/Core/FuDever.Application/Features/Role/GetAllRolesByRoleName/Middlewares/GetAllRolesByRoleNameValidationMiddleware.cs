using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Role.GetAllRolesByRoleName.Middlewares;

/// <summary>
///     Get all roles by role name
///     request validation middleware.
/// </summary>
/// <remarks>
///     Order: 1st
/// </remarks>
internal sealed class GetAllRolesByRoleNameValidationMiddleware :
    IPipelineBehavior<
        GetAllRolesByRoleNameRequest,
        GetAllRolesByRoleNameResponse>,
    IGetAllRolesByRoleNameMiddleware
{
    private readonly IValidator<GetAllRolesByRoleNameRequest> _validator;

    public GetAllRolesByRoleNameValidationMiddleware(IValidator<GetAllRolesByRoleNameRequest> validator)
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
    public async Task<GetAllRolesByRoleNameResponse> Handle(
        GetAllRolesByRoleNameRequest request,
        RequestHandlerDelegate<GetAllRolesByRoleNameResponse> next,
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
                StatusCode = GetAllRolesByRoleNameResponseStatusCode.INPUT_VALIDATION_FAIL,
                FoundRoles = default
            };
        }

        return await next();
    }
}
