using FuDever.Application.Features.Skill.UpdateSkillBySkillId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Skill.UpdateSkillBySkillId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Skill.UpdateSkillBySkillId;

/// <summary>
///     Update skill by skill id response
///     status code - skill already exists
///     http response.
/// </summary>
internal sealed class SkillAlreadyExistsHttpResponse :
    BaseHttpResponse,
    IUpdateSkillBySkillIdHttpResponse
{
    internal SkillAlreadyExistsHttpResponse(UpdateSkillBySkillIdRequest request)
    {
        HttpCode = StatusCodes.Status409Conflict;
        AppCode = SkillAppCode.SKILL_ALREADY_EXISTS;
        ErrorMessages =
        [
            $"Skill with name = {request.NewSkillName} already exists."
        ];
    }
}
