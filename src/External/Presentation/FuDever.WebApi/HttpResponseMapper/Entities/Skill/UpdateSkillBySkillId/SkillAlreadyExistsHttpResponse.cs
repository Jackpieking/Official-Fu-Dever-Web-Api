using FuDever.Application.Features.Skill.UpdateSkillBySkillId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.HttpResponseMapper.Entities.Skill.UpdateSkillBySkillId.Others;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Skill.UpdateSkillBySkillId;

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
