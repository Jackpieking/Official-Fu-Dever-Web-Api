﻿using FuDever.WebApi.AppCodes.Base;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Skill.RemoveSkillPermanentlyBySkillId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Skill.RemoveSkillPermanentlyBySkillId;

/// <summary>
///     Remove skill permanently by skill
///     Id response status code - input
///     validation fail http response.
/// </summary>
internal sealed class InputValidationFailHttpResponse :
    BaseHttpResponse,
    IRemoveSkillPermanentlyBySkillIdHttpResponse
{
    internal InputValidationFailHttpResponse()
    {
        HttpCode = StatusCodes.Status400BadRequest;
        AppCode = BaseAppCode.INVALID_INPUTS;
        ErrorMessages =
        [
            "Input validation fail. Please check your inputs and try again."
        ];
    }
}