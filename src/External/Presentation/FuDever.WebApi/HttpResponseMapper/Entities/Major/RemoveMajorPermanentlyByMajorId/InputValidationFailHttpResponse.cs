﻿using FuDever.WebApi.AppCodes.Base;
using FuDever.WebApi.HttpResponseMapper.Entities.Major.RemoveMajorPermanentlyByMajorId.Others;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Major.RemoveMajorPermanentlyByMajorId;

/// <summary>
///     Remove major permanently by major
///     Id response status code - input
///     validation fail http response.
/// </summary>
internal sealed class InputValidationFailHttpResponse :
    BaseHttpResponse,
    IRemoveMajorPermanentlyByMajorIdHttpResponse
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