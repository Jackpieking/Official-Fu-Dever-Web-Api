using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Department.GetAllDepartments.Middlewares;

/// <summary>
///     Get all departments request validation middleware.
/// </summary>
/// <remarks>
///     Order: 1st
/// </remarks>
internal sealed class GetAllDepartmentsValidationMiddleware :
    IPipelineBehavior<
        GetAllDepartmentsRequest,
        GetAllDepartmentsResponse>,
    IGetAllDepartmentsMiddleware
{
    private readonly IValidator<GetAllDepartmentsRequest> _validator;

    public GetAllDepartmentsValidationMiddleware(IValidator<GetAllDepartmentsRequest> validator)
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
    public async Task<GetAllDepartmentsResponse> Handle(
        GetAllDepartmentsRequest request,
        RequestHandlerDelegate<GetAllDepartmentsResponse> next,
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
                StatusCode = GetAllDepartmentsResponseStatusCode.INPUT_VALIDATION_FAIL,
                FoundDepartments = default
            };
        }

        return await next();
    }
}
