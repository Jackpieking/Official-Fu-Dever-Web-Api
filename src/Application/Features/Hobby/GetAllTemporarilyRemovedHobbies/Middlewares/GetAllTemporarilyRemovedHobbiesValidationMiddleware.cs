using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace Application.Features.Hobby.GetAllTemporarilyRemovedHobbies.Middlewares;

/// <summary>
///     Get all temporarily removed hobbies
///     request validation middleware.
/// </summary>
/// <remarks>
///     Order: 1st
/// </remarks>
internal sealed class GetAllTemporarilyRemovedHobbiesValidationMiddleware :
    IPipelineBehavior<
        GetAllTemporarilyRemovedHobbiesRequest,
        GetAllTemporarilyRemovedHobbiesResponse>,
    IGetAllTemporarilyRemovedHobbiesMiddleware
{
    private readonly IValidator<GetAllTemporarilyRemovedHobbiesRequest> _validator;

    public GetAllTemporarilyRemovedHobbiesValidationMiddleware(IValidator<GetAllTemporarilyRemovedHobbiesRequest> validator)
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
    public async Task<GetAllTemporarilyRemovedHobbiesResponse> Handle(
        GetAllTemporarilyRemovedHobbiesRequest request,
        RequestHandlerDelegate<GetAllTemporarilyRemovedHobbiesResponse> next,
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
                StatusCode = GetAllTemporarilyRemovedHobbiesResponseStatusCode.INPUT_VALIDATION_FAIL,
                FoundTemporarilyRemovedHobbies = default
            };
        }

        return await next();
    }
}
