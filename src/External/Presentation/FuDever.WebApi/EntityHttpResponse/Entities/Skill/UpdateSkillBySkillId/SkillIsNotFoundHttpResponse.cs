using FuDever.Application.Features.Skill.UpdateSkillBySkillId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Skill.UpdateSkillBySkillId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Skill.UpdateSkillBySkillId;

/// <summary>
///     Update skill by skill
///     Id response status code - skill is not
///     found http response.
/// </summary>
internal sealed class SkillIsNotFoundHttpResponse :
    BaseHttpResponse,
    IUpdateSkillBySkillIdHttpResponse
{
    internal SkillIsNotFoundHttpResponse(UpdateSkillBySkillIdRequest request)
    {
        HttpCode = StatusCodes.Status404NotFound;
        AppCode = SkillAppCode.SKILL_IS_NOT_FOUND;
        ErrorMessages =
        [
            $"Skill with Id = {request.SkillId} is not found."
        ];
    }
}
