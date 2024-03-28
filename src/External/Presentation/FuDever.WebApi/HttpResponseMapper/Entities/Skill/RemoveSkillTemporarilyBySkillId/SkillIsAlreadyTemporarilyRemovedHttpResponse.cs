using FuDever.Application.Features.Skill.RemoveSkillTemporarilyBySkillId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.HttpResponseMapper.Entities.Skill.RemoveSkillTemporarilyBySkillId.Others;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Skill.RemoveSkillTemporarilyBySkillId;

/// <summary>
///     Remove skill temporarily by skill
///     Id response status code - skill is already
///     temporarily removed http response.
/// </summary>
internal sealed class SkillIsAlreadyTemporarilyRemovedHttpResponse :
    BaseHttpResponse,
    IRemoveSkillTemporarilyBySkillIdHttpResponse
{
    internal SkillIsAlreadyTemporarilyRemovedHttpResponse(RemoveSkillTemporarilyBySkillIdRequest request)
    {
        HttpCode = StatusCodes.Status400BadRequest;
        AppCode = SkillAppCode.SKILL_IS_ALREADY_TEMPORARILY_REMOVED;
        ErrorMessages =
        [
            $"Found skill with Id = {request.SkillId} in temporarily removed storage."
        ];
    }
}
