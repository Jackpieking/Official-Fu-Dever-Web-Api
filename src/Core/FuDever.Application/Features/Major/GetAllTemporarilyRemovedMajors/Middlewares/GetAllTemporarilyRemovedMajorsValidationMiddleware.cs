using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Major.GetAllTemporarilyRemovedMajors.Middlewares;

/// <summary>
///     Get all temporarily removed majors
///     request validation middleware.
/// </summary>
/// <remarks>
///     Order: 1st
/// </remarks>
internal sealed class GetAllTemporarilyRemovedMajorsValidationMiddleware :
    IPipelineBehavior<
        GetAllTemporarilyRemovedMajorsRequest,
        GetAllTemporarilyRemovedMajorsResponse>,
    IGetAllTemporarilyRemovedMajorsMiddleware
{
    private readonly IValidator<GetAllTemporarilyRemovedMajorsRequest> _validator;

    public GetAllTemporarilyRemovedMajorsValidationMiddleware(IValidator<GetAllTemporarilyRemovedMajorsRequest> validator)
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
    public async Task<GetAllTemporarilyRemovedMajorsResponse> Handle(
        GetAllTemporarilyRemovedMajorsRequest request,
        RequestHandlerDelegate<GetAllTemporarilyRemovedMajorsResponse> next,
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
                StatusCode = GetAllTemporarilyRemovedMajorsResponseStatusCode.INPUT_VALIDATION_FAIL,
                FoundTemporarilyRemovedMajors = default
            };
        }

        return await next();
    }
}
