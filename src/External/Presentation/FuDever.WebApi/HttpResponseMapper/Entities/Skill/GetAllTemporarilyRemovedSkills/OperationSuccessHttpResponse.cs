using FuDever.Application.Features.Skill.GetAllTemporarilyRemovedSkills;
using FuDever.WebApi.AppCodes.Base;
using FuDever.WebApi.HttpResponseMapper.Entities.Skill.GetAllTemporarilyRemovedSkills.Others;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Skill.GetAllTemporarilyRemovedSkills;

/// <summary>
///     Get all temporarily removed skills response status code
///     - operation success http response.
/// </summary>
internal sealed class OperationSuccessHttpResponse :
    BaseHttpResponse,
    IGetAllTemporarilyRemovedSkillsHttpResponse
{
    internal OperationSuccessHttpResponse(GetAllTemporarilyRemovedSkillsResponse response)
    {
        HttpCode = StatusCodes.Status200OK;
        AppCode = BaseAppCode.SUCCESS;
        Body = response.FoundTemporarilyRemovedSkills;
    }
}
