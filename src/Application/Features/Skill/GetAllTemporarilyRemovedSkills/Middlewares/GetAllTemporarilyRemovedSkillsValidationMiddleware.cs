using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Skill.GetAllTemporarilyRemovedSkills.Middlewares;

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
                StatusCode = GetAllTemporarilyRemovedSkillsStatusCode.INPUT_VALIDATION_FAIL,
                FoundTemporarilyRemovedSkills = default
            };
        }

        return await next();
    }
}
