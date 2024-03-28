using FuDever.Application.Features.Major.CreateMajor;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.HttpResponseMapper.Entities.Major.CreateMajor.Others;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Major.CreateMajor;

/// <summary>
///     Create major response status code
///     - major is already temporarily removed
///     http response.
/// </summary>
internal sealed class MajorIsAlreadyTemporarilyRemovedHttpResponse :
    BaseHttpResponse,
    ICreateMajorHttpResponse
{
    internal MajorIsAlreadyTemporarilyRemovedHttpResponse(CreateMajorRequest request)
    {
        HttpCode = StatusCodes.Status400BadRequest;
        AppCode = MajorAppCode.MAJOR_IS_ALREADY_TEMPORARILY_REMOVED;
        ErrorMessages =
        [
            $"Found major with name = {request.NewMajorName} in temporarily removed storage."
        ];
    }
}
