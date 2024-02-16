using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace Application.Features.Skill.UpdateSkillBySkillId.Middlewares;

/// <summary>
///     Update skill by skill id
///     request validation middleware.
/// </summary>
/// <remarks>
///     Order: 1st
/// </remarks>
internal sealed class UpdateSkillBySkillIdValidationMiddleware :
    IPipelineBehavior<
        UpdateSkillBySkillIdRequest,
        UpdateSkillBySkillIdResponse>,
    IUpdateSkillBySkillIdMiddleware
{
    private readonly IValidator<UpdateSkillBySkillIdRequest> _validator;

    public UpdateSkillBySkillIdValidationMiddleware(IValidator<UpdateSkillBySkillIdRequest> validator)
    {
        _validator = validator;
    }

    public async Task<UpdateSkillBySkillIdResponse> Handle(
        UpdateSkillBySkillIdRequest request,
        RequestHandlerDelegate<UpdateSkillBySkillIdResponse> next,
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
                StatusCode = UpdateSkillBySkillIdStatusCode.INPUT_VALIDATION_FAIL,
            };
        }

        return await next();
    }
}