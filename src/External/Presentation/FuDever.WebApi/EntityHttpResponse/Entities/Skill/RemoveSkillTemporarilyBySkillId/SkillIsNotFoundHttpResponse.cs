using FuDever.Application.Features.Skill.RemoveSkillTemporarilyBySkillId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Skill.RemoveSkillTemporarilyBySkillId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Skill.RemoveSkillTemporarilyBySkillId;

/// <summary>
///     Remove skill temporarily by skill
///     Id response status code - skill is not
///     found http response.
/// </summary>
internal sealed class SkillIsNotFoundHttpResponse :
    BaseHttpResponse,
    IRemoveSkillTemporarilyBySkillIdHttpResponse
{
    internal SkillIsNotFoundHttpResponse(RemoveSkillTemporarilyBySkillIdRequest request)
    {
        HttpCode = StatusCodes.Status404NotFound;
        AppCode = SkillAppCode.SKILL_IS_NOT_FOUND;
        ErrorMessages =
        [
            $"Skill with Id = {request.SkillId} is not found."
        ];
    }
}
