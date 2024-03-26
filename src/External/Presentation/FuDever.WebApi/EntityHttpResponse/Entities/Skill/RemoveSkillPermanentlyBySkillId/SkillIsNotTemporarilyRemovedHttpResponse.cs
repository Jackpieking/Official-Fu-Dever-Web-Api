using FuDever.Application.Features.Skill.RemoveSkillPermanentlyBySkillId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Skill.RemoveSkillPermanentlyBySkillId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Skill.RemoveSkillPermanentlyBySkillId;

/// <summary>
///     Remove skill permanently by skill
///     Id response status code - skill id not
///     found http response.
/// </summary>
internal sealed class SkillIsNotTemporarilyRemovedHttpResponse :
    BaseHttpResponse,
    IRemoveSkillPermanentlyBySkillIdHttpResponse
{
    internal SkillIsNotTemporarilyRemovedHttpResponse(RemoveSkillPermanentlyBySkillIdRequest request)
    {
        HttpCode = StatusCodes.Status400BadRequest;
        AppCode = SkillAppCode.SKILL_IS_ALREADY_TEMPORARILY_REMOVED;
        ErrorMessages =
        [
            $"Skill with Id = {request.SkillId} is not found in temporarily removed storage."
        ];
    }
}
