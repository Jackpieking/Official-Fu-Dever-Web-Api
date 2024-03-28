using FuDever.Application.Features.Major.CreateMajor;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.HttpResponseMapper.Entities.Major.CreateMajor.Others;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Major.CreateMajor;

/// <summary>
///     Create major response status code
///     - major already exists http response.
/// </summary>
internal sealed class MajorAlreadyExistsHttpResponse :
    BaseHttpResponse,
    ICreateMajorHttpResponse
{
    internal MajorAlreadyExistsHttpResponse(CreateMajorRequest request)
    {
        HttpCode = StatusCodes.Status409Conflict;
        AppCode = MajorAppCode.MAJOR_ALREADY_EXISTS;
        ErrorMessages =
        [
            $"Major with name = {request.NewMajorName} already exists."
        ];
    }
}
