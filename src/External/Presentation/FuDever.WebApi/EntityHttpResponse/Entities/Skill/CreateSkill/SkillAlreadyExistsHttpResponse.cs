using FuDever.Application.Features.Skill.CreateSkill;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Skill.CreateSkill.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Skill.CreateSkill;

/// <summary>
///     Create skill response status code
///     - position already exists http response.
/// </summary>
internal sealed class SkillAlreadyExistsHttpResponse :
    BaseHttpResponse,
    ICreateSkillHttpResponse
{
    internal SkillAlreadyExistsHttpResponse(CreateSkillRequest request)
    {
        HttpCode = StatusCodes.Status409Conflict;
        AppCode = SkillAppCode.SKILL_ALREADY_EXISTS;
        ErrorMessages =
        [
            $"Skill with name = {request.NewSkillName} already exists."
        ];
    }
}
