﻿using FuDever.WebApi.AppCodes.Base;
using FuDever.WebApi.HttpResponseMapper.Entities.Major.GetAllMajorsByMajorName.Others;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Major.GetAllMajorsByMajorName;

/// <summary>
///     Get all majors by major name response status code
///     - input validation fail http response.
/// </summary>
internal sealed class InputValidationFailHttpResponse :
    BaseHttpResponse,
    IGetAllMajorsByMajorNameHttpResponse
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
