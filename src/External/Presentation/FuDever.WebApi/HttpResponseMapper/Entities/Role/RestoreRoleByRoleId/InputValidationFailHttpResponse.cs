﻿using FuDever.WebApi.AppCodes.Base;
using FuDever.WebApi.HttpResponseMapper.Entities.Role.RestoreRoleByRoleId.Others;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Role.RestoreRoleByRoleId;

/// <summary>
///     Restore role by role
///     Id response status code - input validation
///     fail http response.
/// </summary>
internal sealed class InputValidationFailHttpResponse :
    BaseHttpResponse,
    IRestoreRoleByRoleIdHttpResponse
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