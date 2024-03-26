using FuDever.Application.Features.Major.RemoveMajorTemporarilyByMajorId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Major.RemoveMajorTemporarilyByMajorId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Major.RemoveMajorTemporarilyByMajorId;

/// <summary>
///     Remove major temporarily by major
///     Id response status code - major is not
///     found http response.
/// </summary>
internal sealed class MajorIsNotFoundHttpResponse :
    BaseHttpResponse,
    IRemoveMajorTemporarilyByMajorIdHttpResponse
{
    internal MajorIsNotFoundHttpResponse(RemoveMajorTemporarilyByMajorIdRequest request)
    {
        HttpCode = StatusCodes.Status404NotFound;
        AppCode = MajorAppCode.MAJOR_IS_NOT_FOUND;
        ErrorMessages =
        [
            $"Major with Id = {request.MajorId} is not found."
        ];
    }
}
