using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Department.RemoveDepartmentTemporarilyByDepartmentId.Middlewares;

/// <summary>
///     Remove department temporarily by department
///     id request validation middleware.
/// </summary>
/// <remarks>
///     Order: 1st
/// </remarks>
internal sealed class RemoveDepartmentTemporarilyByDepartmentIdValidationMiddleware :
    IPipelineBehavior<
        RemoveDepartmentTemporarilyByDepartmentIdRequest,
        RemoveDepartmentTemporarilyByDepartmentIdResponse>,
    IRemoveDepartmentTemporarilyByDepartmentIdMiddleware
{
    private readonly IValidator<RemoveDepartmentTemporarilyByDepartmentIdRequest> _validator;

    public RemoveDepartmentTemporarilyByDepartmentIdValidationMiddleware(IValidator<RemoveDepartmentTemporarilyByDepartmentIdRequest> validator)
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
    public async Task<RemoveDepartmentTemporarilyByDepartmentIdResponse> Handle(
        RemoveDepartmentTemporarilyByDepartmentIdRequest request,
        RequestHandlerDelegate<RemoveDepartmentTemporarilyByDepartmentIdResponse> next,
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
                StatusCode = RemoveDepartmentTemporarilyByDepartmentIdResponseStatusCode.INPUT_VALIDATION_FAIL
            };
        }

        return await next();
    }
}
