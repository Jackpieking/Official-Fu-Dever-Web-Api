using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace Application.Features.Skill.RestoreSkillBySkillId.Middlewares;

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
                StatusCode = RestoreSkillBySkillIdStatusCode.INPUT_VALIDATION_FAIL
            };
        }

        return await next();
    }
}
