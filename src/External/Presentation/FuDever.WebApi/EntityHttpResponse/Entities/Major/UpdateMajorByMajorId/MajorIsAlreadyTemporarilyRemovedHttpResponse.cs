using FuDever.Application.Features.Major.UpdateMajorByMajorId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Major.UpdateMajorByMajorId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Major.UpdateMajorByMajorId;

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
