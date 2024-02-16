using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace Application.Features.Skill.CreateSkill.Middlewares;

/// <summary>
///     Create skill request validation middleware.
/// </summary>
/// <remarks>
///     Order: 2nd
/// </remarks>
internal sealed class CreateSkillValidationMiddleware :
    IPipelineBehavior<
        CreateSkillRequest,
        CreateSkillResponse>,
    ICreateSkillMiddleware
{
    private readonly IValidator<CreateSkillRequest> _validator;

    public CreateSkillValidationMiddleware(IValidator<CreateSkillRequest> validator)
    {
        _validator = validator;
    }

    public async Task<CreateSkillResponse> Handle(
        CreateSkillRequest request,
        RequestHandlerDelegate<CreateSkillResponse> next,
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
                StatusCode = CreateSkillStatusCode.INPUT_VALIDATION_FAIL
            };
        }

        return await next();
    }
}
