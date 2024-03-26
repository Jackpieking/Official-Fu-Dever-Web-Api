﻿using FuDever.WebApi.AppCodes.Base;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Skill.RestoreSkillBySkillId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Skill.RestoreSkillBySkillId;

/// <summary>
///     Restore skill by skill
///     Id response status code - database operation
///     fail http response.
/// </summary>
internal sealed class DatabaseOperationFailHttpResponse :
    BaseHttpResponse,
    IRestoreSkillBySkillIdHttpResponse
{
    internal DatabaseOperationFailHttpResponse()
    {
        HttpCode = StatusCodes.Status500InternalServerError;
        AppCode = BaseAppCode.SERVER_ERROR;
        ErrorMessages =
        [
            "Database operations failed."
        ];
    }
}