using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace Application.Features.Skill.RemoveSkillPermanentlyBySkillId.Middlewares;

/// <summary>
///     Remove skill permanently by skill id
///     request validation middleware.
/// </summary>
/// <remarks>
///     Order: 1st
/// </remarks>
internal sealed class RemoveSkillPermanentlyBySkillIdValidationMiddleware :
    IPipelineBehavior<
        RemoveSkillPermanentlyBySkillIdRequest,
        RemoveSkillPermanentlyBySkillIdResponse>,
    IRemoveSkillPermanentlyBySkillIdMiddleware
{
    private readonly IValidator<RemoveSkillPermanentlyBySkillIdRequest> _validator;

    public RemoveSkillPermanentlyBySkillIdValidationMiddleware(IValidator<RemoveSkillPermanentlyBySkillIdRequest> validator)
    {
        _validator = validator;
    }

    public async Task<RemoveSkillPermanentlyBySkillIdResponse> Handle(
        RemoveSkillPermanentlyBySkillIdRequest request,
        RequestHandlerDelegate<RemoveSkillPermanentlyBySkillIdResponse> next,
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
                StatusCode = RemoveSkillPermanentlyBySkillIdStatusCode.INPUT_VALIDATION_FAIL
            };
        }

        return await next();
    }
}
