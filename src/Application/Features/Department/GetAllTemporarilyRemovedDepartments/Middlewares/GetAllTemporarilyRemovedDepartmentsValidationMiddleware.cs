using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Department.GetAllTemporarilyRemovedDepartments.Middlewares;

/// <summary>
///     Get all temporarily removed departments
///     request validation middleware.
/// </summary>
/// <remarks>
///     Order: 1st
/// </remarks>
internal sealed class GetAllTemporarilyRemovedDepartmentsValidationMiddleware :
    IPipelineBehavior<
        GetAllTemporarilyRemovedDepartmentsRequest,
        GetAllTemporarilyRemovedDepartmentsResponse>,
    IGetAllTemporarilyRemovedDepartmentsMiddleware
{
    private readonly IValidator<GetAllTemporarilyRemovedDepartmentsRequest> _validator;

    public GetAllTemporarilyRemovedDepartmentsValidationMiddleware(IValidator<GetAllTemporarilyRemovedDepartmentsRequest> validator)
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
    public async Task<GetAllTemporarilyRemovedDepartmentsResponse> Handle(
        GetAllTemporarilyRemovedDepartmentsRequest request,
        RequestHandlerDelegate<GetAllTemporarilyRemovedDepartmentsResponse> next,
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
                StatusCode = GetAllTemporarilyRemovedDepartmentsResponseStatusCode.INPUT_VALIDATION_FAIL,
                FoundTemporarilyRemovedDepartments = default
            };
        }

        return await next();
    }
}
