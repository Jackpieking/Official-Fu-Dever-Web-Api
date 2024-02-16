using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace Application.Features.Skill.GetAllSkills.Middlewares;

/// <summary>
///     Get all skills request validation middleware.
/// </summary>
/// <remarks>
///     Order: 1st
/// </remarks>
internal sealed class GetAllSkillsValidationMiddleware :
    IPipelineBehavior<
        GetAllSkillsRequest,
        GetAllSkillsResponse>,
    IGetAllSkillsMiddleware
{
    private readonly IValidator<GetAllSkillsRequest> _validator;

    public GetAllSkillsValidationMiddleware(IValidator<GetAllSkillsRequest> validator)
    {
        _validator = validator;
    }

    public async Task<GetAllSkillsResponse> Handle(
        GetAllSkillsRequest request,
        RequestHandlerDelegate<GetAllSkillsResponse> next,
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
                StatusCode = GetAllSkillsStatusCode.INPUT_VALIDATION_FAIL,
                FoundSkills = default
            };
        }

        return await next();
    }
}
