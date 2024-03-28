using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace FuDever.Application.Features.Auth.Login.Middlewares;

/// <summary>
///     Login request validation middleware.
/// </summary>
/// <remarks>
///     Order: 1st
/// </remarks>
internal sealed class LoginValidationMiddleware :
    IPipelineBehavior<
        LoginRequest,
        LoginResponse>,
    ILoginMiddleware
{
    private readonly IValidator<LoginRequest> _validator;

    public LoginValidationMiddleware(IValidator<LoginRequest> validator)
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
    public async Task<LoginResponse> Handle(
        LoginRequest request,
        RequestHandlerDelegate<LoginResponse> next,
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
                StatusCode = LoginResponseStatusCode.INPUT_VALIDATION_FAIL
            };
        }

        return await next();
    }
}
