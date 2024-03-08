using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Department.CreateDepartment.Middlewares;

/// <summary>
///     Create department request validation middleware.
/// </summary>
/// <remarks>
///     Order: 1st
/// </remarks>
internal sealed class CreateDepartmentValidationMiddleware :
    IPipelineBehavior<
        CreateDepartmentRequest,
        CreateDepartmentResponse>,
    ICreateDepartmentMiddleware
{
    private readonly IValidator<CreateDepartmentRequest> _validator;

    public CreateDepartmentValidationMiddleware(IValidator<CreateDepartmentRequest> validator)
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
    public async Task<CreateDepartmentResponse> Handle(
        CreateDepartmentRequest request,
        RequestHandlerDelegate<CreateDepartmentResponse> next,
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
                StatusCode = CreateDepartmentResponseStatusCode.INPUT_VALIDATION_FAIL
            };
        }

        return await next();
    }
}
