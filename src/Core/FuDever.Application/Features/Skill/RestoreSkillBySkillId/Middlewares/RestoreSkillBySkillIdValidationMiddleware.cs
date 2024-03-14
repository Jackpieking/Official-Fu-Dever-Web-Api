using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Skill.RestoreSkillBySkillId.Middlewares;

/// <summary>
///     Restore skill by skill
///     id request validation middleware.
/// </summary>
/// <remarks>
///     Order: 1st
/// </remarks>
internal sealed class RestoreSkillBySkillIdValidationMiddleware :
    IPipelineBehavior<
        RestoreSkillBySkillIdRequest,
        RestoreSkillBySkillIdResponse>,
    IRestoreSkillBySkillIdMiddleware
{
    private readonly IValidator<RestoreSkillBySkillIdRequest> _validator;

    public RestoreSkillBySkillIdValidationMiddleware(IValidator<RestoreSkillBySkillIdRequest> validator)
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
    public async Task<RestoreSkillBySkillIdResponse> Handle(
        RestoreSkillBySkillIdRequest request,
        RequestHandlerDelegate<RestoreSkillBySkillIdResponse> next,
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
                StatusCode = RestoreSkillBySkillIdResponseStatusCode.INPUT_VALIDATION_FAIL
            };
        }

        return await next();
    }
}
