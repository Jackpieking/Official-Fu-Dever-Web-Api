using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace Application.Features.Skill.RemoveSkillTemporarilyBySkillId.Middlewares;

/// <summary>
///     Remove skill temporarily by skill
///     id request validation middleware.
/// </summary>
/// <remarks>
///     Order: 1st
/// </remarks>
internal sealed class RemoveSkillTemporarilyBySkillIdValidationMiddleware :
    IPipelineBehavior<
        RemoveSkillTemporarilyBySkillIdRequest,
        RemoveSkillTemporarilyBySkillIdResponse>,
    IRemoveSkillTemporarilyBySkillIdMiddleware
{
    private readonly IValidator<RemoveSkillTemporarilyBySkillIdRequest> _validator;

    public RemoveSkillTemporarilyBySkillIdValidationMiddleware(IValidator<RemoveSkillTemporarilyBySkillIdRequest> validator)
    {
        _validator = validator;
    }

    public async Task<RemoveSkillTemporarilyBySkillIdResponse> Handle(
        RemoveSkillTemporarilyBySkillIdRequest request, RequestHandlerDelegate<RemoveSkillTemporarilyBySkillIdResponse> next,
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
                StatusCode = RemoveSkillTemporarilyBySkillIdStatusCode.INPUT_VALIDATION_FAIL
            };
        }

        return await next();
    }
}