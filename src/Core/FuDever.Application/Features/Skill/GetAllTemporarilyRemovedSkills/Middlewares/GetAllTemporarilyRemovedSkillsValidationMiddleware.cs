using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Skill.GetAllTemporarilyRemovedSkills.Middlewares;

/// <summary>
///     Get all temporarily removed skills
///     request validation middleware.
/// </summary>
/// <remarks>
///     Order: 1st
/// </remarks>
internal sealed class GetAllTemporarilyRemovedSkillsValidationMiddleware :
    IPipelineBehavior<
        GetAllTemporarilyRemovedSkillsRequest,
        GetAllTemporarilyRemovedSkillsResponse>,
    IGetAllTemporarilyRemovedSkillsMiddleware
{
    private readonly IValidator<GetAllTemporarilyRemovedSkillsRequest> _validator;

    public GetAllTemporarilyRemovedSkillsValidationMiddleware(IValidator<GetAllTemporarilyRemovedSkillsRequest> validator)
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
    public async Task<GetAllTemporarilyRemovedSkillsResponse> Handle(
        GetAllTemporarilyRemovedSkillsRequest request,
        RequestHandlerDelegate<GetAllTemporarilyRemovedSkillsResponse> next,
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
                StatusCode = GetAllTemporarilyRemovedSkillsResponseStatusCode.INPUT_VALIDATION_FAIL,
                FoundTemporarilyRemovedSkills = default
            };
        }

        return await next();
    }
}
