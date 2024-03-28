using FuDever.Application.Features.Major.UpdateMajorByMajorId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.HttpResponseMapper.Entities.Major.UpdateMajorByMajorId.Others;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Major.UpdateMajorByMajorId;

/// <summary>
///     Update major by major
///     Id response status code - major is already
///     temporarily removed http response.
/// </summary>
internal sealed class MajorIsAlreadyTemporarilyRemovedHttpResponse :
    BaseHttpResponse,
    IUpdateMajorByMajorIdHttpResponse
{
    internal MajorIsAlreadyTemporarilyRemovedHttpResponse(UpdateMajorByMajorIdRequest request)
    {
        HttpCode = StatusCodes.Status400BadRequest;
        AppCode = MajorAppCode.MAJOR_IS_ALREADY_TEMPORARILY_REMOVED;
        ErrorMessages =
        [
            $"Found major with Id = {request.MajorId} in temporarily removed storage."
        ];
    }
}
