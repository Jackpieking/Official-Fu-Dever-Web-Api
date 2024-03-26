using FuDever.Application.Features.Skill.RemoveSkillPermanentlyBySkillId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Skill.RemoveSkillPermanentlyBySkillId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Skill.RemoveSkillPermanentlyBySkillId;

/// <summary>
///     Remove skill permanently by skill
///     Id response status code - skill is not
///     found http response.
/// </summary>
internal sealed class SkillIsNotFoundHttpResponse :
    BaseHttpResponse,
    IRemoveSkillPermanentlyBySkillIdHttpResponse
{
    internal SkillIsNotFoundHttpResponse(RemoveSkillPermanentlyBySkillIdRequest request)
    {
        HttpCode = StatusCodes.Status404NotFound;
        AppCode = SkillAppCode.SKILL_IS_NOT_FOUND;
        ErrorMessages =
        [
            $"Skill with Id = {request.SkillId} is not found."
        ];
    }
}
