using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

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
                StatusCode = RemoveSkillPermanentlyBySkillIdResponseStatusCode.INPUT_VALIDATION_FAIL
            };
        }

        return await next();
    }
}
