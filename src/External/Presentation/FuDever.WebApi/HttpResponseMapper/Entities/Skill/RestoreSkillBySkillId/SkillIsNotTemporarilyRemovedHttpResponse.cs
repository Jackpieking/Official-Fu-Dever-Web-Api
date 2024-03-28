using FuDever.Application.Features.Skill.RestoreSkillBySkillId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.HttpResponseMapper.Entities.Skill.RestoreSkillBySkillId.Others;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Skill.RestoreSkillBySkillId;

/// <summary>
///     Restore skill by skill
///     Id response status code - skill id not
///     found http response.
/// </summary>
internal sealed class SkillIsNotTemporarilyRemovedHttpResponse :
    BaseHttpResponse,
    IRestoreSkillBySkillIdHttpResponse
{
    internal SkillIsNotTemporarilyRemovedHttpResponse(RestoreSkillBySkillIdRequest request)
    {
        HttpCode = StatusCodes.Status400BadRequest;
        AppCode = SkillAppCode.SKILL_IS_NOT_TEMPORARILY_REMOVED;
        ErrorMessages =
        [
            $"Skill with Id = {request.SkillId} is not found in temporarily removed storage."
        ];
    }
}
