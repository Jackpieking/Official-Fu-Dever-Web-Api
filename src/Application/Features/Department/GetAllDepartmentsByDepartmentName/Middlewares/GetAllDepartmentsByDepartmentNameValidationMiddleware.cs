using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace Application.Features.Department.GetAllDepartmentsByDepartmentName.Middlewares;

/// <summary>
///     Get all departments by department name
///     request validation middleware.
/// </summary>
/// <remarks>
///     Order: 1st
/// </remarks>
internal sealed class GetAllDepartmentsByDepartmentNameValidationMiddleware :
    IPipelineBehavior<
        GetAllDepartmentsByDepartmentNameRequest,
        GetAllDepartmentsByDepartmentNameResponse>,
    IGetAllDepartmentsByDepartmentNameMiddleware
{
    private readonly IValidator<GetAllDepartmentsByDepartmentNameRequest> _validator;

    public GetAllDepartmentsByDepartmentNameValidationMiddleware(IValidator<GetAllDepartmentsByDepartmentNameRequest> validator)
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
    public async Task<GetAllDepartmentsByDepartmentNameResponse> Handle(
        GetAllDepartmentsByDepartmentNameRequest request,
        RequestHandlerDelegate<GetAllDepartmentsByDepartmentNameResponse> next,
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
                StatusCode = GetAllDepartmentsByDepartmentNameStatusCode.INPUT_VALIDATION_FAIL,
                FoundDepartments = default
            };
        }

        return await next();
    }
}
