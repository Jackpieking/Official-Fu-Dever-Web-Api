using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Department.UpdateDepartmentByDepartmentId.Middlewares;

/// <summary>
///     Update department by department id
///     request validation middleware.
/// </summary>
/// <remarks>
///     Order: 1st
/// </remarks>
internal sealed class UpdateDepartmentByDepartmentIdValidationMiddleware :
    IPipelineBehavior<
        UpdateDepartmentByDepartmentIdRequest,
        UpdateDepartmentByDepartmentIdResponse>,
    IUpdateDepartmentByDepartmentIdMiddleware
{
    private readonly IValidator<UpdateDepartmentByDepartmentIdRequest> _validator;

    public UpdateDepartmentByDepartmentIdValidationMiddleware(IValidator<UpdateDepartmentByDepartmentIdRequest> validator)
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

    public async Task<UpdateDepartmentByDepartmentIdResponse> Handle(
        UpdateDepartmentByDepartmentIdRequest request,
        RequestHandlerDelegate<UpdateDepartmentByDepartmentIdResponse> next,
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
                StatusCode = UpdateDepartmentByDepartmentIdStatusCode.INPUT_VALIDATION_FAIL,
            };
        }

        return await next();
    }
}
