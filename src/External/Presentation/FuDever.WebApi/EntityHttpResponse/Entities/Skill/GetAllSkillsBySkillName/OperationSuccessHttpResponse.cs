using FuDever.Application.Features.Skill.GetAllSkillsBySkillName;
using FuDever.WebApi.AppCodes.Base;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Skill.GetAllSkillsBySkillName.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Skill.GetAllSkillsBySkillName;

/// <summary>
///     Get all skills by skill name response status code
///     - operation success http response.
/// </summary>
internal sealed class OperationSuccessHttpResponse :
    BaseHttpResponse,
    IGetAllSkillsBySkillNameHttpResponse
{
    internal OperationSuccessHttpResponse(GetAllSkillsBySkillNameResponse response)
    {
        HttpCode = StatusCodes.Status200OK;
        AppCode = BaseAppCode.SUCCESS;
        Body = response.FoundSkills;
    }
}
