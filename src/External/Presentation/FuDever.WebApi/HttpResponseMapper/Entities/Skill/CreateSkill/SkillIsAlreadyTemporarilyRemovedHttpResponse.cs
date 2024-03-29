﻿using FuDever.Application.Features.Skill.CreateSkill;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.HttpResponseMapper.Entities.Skill.CreateSkill.Others;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Skill.CreateSkill;

/// <summary>
///     Create skill response status code
///     - skill is already temporarily removed
///     http response.
/// </summary>
internal sealed class SkillIsAlreadyTemporarilyRemovedHttpResponse :
    BaseHttpResponse,
    ICreateSkillHttpResponse
{
    internal SkillIsAlreadyTemporarilyRemovedHttpResponse(CreateSkillRequest request)
    {
        HttpCode = StatusCodes.Status400BadRequest;
        AppCode = SkillAppCode.SKILL_IS_ALREADY_TEMPORARILY_REMOVED;
        ErrorMessages =
        [
            $"Found skill with name = {request.NewSkillName} in temporarily removed storage."
        ];
    }
}
