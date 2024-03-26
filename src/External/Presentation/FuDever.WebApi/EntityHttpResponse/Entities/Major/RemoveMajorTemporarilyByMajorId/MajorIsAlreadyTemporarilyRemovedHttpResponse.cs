using FuDever.Application.Features.Major.RemoveMajorTemporarilyByMajorId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Major.RemoveMajorTemporarilyByMajorId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Major.RemoveMajorTemporarilyByMajorId;

/// <summary>
///     Remove major temporarily by major
///     Id response status code - major is already
///     temporarily removed http response.
/// </summary>
internal sealed class MajorIsAlreadyTemporarilyRemovedHttpResponse :
    BaseHttpResponse,
    IRemoveMajorTemporarilyByMajorIdHttpResponse
{
    internal MajorIsAlreadyTemporarilyRemovedHttpResponse(RemoveMajorTemporarilyByMajorIdRequest request)
    {
        HttpCode = StatusCodes.Status400BadRequest;
        AppCode = MajorAppCode.MAJOR_IS_ALREADY_TEMPORARILY_REMOVED;
        ErrorMessages =
        [
            $"Found major with Id = {request.MajorId} in temporarily removed storage."
        ];
    }
}
