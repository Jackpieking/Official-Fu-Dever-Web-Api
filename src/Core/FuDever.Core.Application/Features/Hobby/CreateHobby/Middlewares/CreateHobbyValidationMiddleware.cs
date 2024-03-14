using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Hobby.CreateHobby.Middlewares;

/// <summary>
///     Create hobby request validation middleware.
/// </summary>
/// <remarks>
///     Order: 1st
/// </remarks>
internal sealed class CreateHobbyValidationMiddleware :
    IPipelineBehavior<
        CreateHobbyRequest,
        CreateHobbyResponse>,
    ICreateHobbyMiddleware
{
    private readonly IValidator<CreateHobbyRequest> _validator;

    public CreateHobbyValidationMiddleware(IValidator<CreateHobbyRequest> validator)
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

    public async Task<CreateHobbyResponse> Handle(
        CreateHobbyRequest request,
        RequestHandlerDelegate<CreateHobbyResponse> next,
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
                StatusCode = CreateHobbyResponseStatusCode.INPUT_VALIDATION_FAIL
            };
        }

        return await next();
    }
}
