using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Major.CreateMajor.Middlewares;

/// <summary>
///     Create major request validation middleware.
/// </summary>
/// <remarks>
///     Order: 1st
/// </remarks>
internal sealed class CreateMajorValidationMiddleware :
    IPipelineBehavior<
        CreateMajorRequest,
        CreateMajorResponse>,
    ICreateMajorMiddleware
{
    private readonly IValidator<CreateMajorRequest> _validator;

    public CreateMajorValidationMiddleware(IValidator<CreateMajorRequest> validator)
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
    public async Task<CreateMajorResponse> Handle(
        CreateMajorRequest request,
        RequestHandlerDelegate<CreateMajorResponse> next,
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
                StatusCode = CreateMajorResponseStatusCode.INPUT_VALIDATION_FAIL
            };
        }

        return await next();
    }
}
