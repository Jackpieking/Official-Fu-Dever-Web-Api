using FuDever.Application.Features.Skill.UpdateSkillBySkillId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Skill.UpdateSkillBySkillId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Skill.UpdateSkillBySkillId;

/// <summary>
///     Update skill by skill
///     Id response status code - skill is already
///     temporarily removed http response.
/// </summary>
internal sealed class SkillIsAlreadyTemporarilyRemovedHttpResponse :
    BaseHttpResponse,
    IUpdateSkillBySkillIdHttpResponse
{
    internal SkillIsAlreadyTemporarilyRemovedHttpResponse(UpdateSkillBySkillIdRequest request)
    {
        HttpCode = StatusCodes.Status400BadRequest;
        AppCode = SkillAppCode.SKILL_IS_ALREADY_TEMPORARILY_REMOVED;
        ErrorMessages =
        [
            $"Found skill with Id = {request.SkillId} in temporarily removed storage."
        ];
    }
}
