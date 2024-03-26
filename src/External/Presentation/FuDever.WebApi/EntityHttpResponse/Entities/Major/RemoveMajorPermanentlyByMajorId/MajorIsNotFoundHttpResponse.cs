using FuDever.Application.Features.Major.RemoveMajorPermanentlyByMajorId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Major.RemoveMajorPermanentlyByMajorId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Major.RemoveMajorPermanentlyByMajorId;

/// <summary>
///     Remove major permanently by major
///     Id response status code - major is not
///     found http response.
/// </summary>
internal sealed class MajorIsNotFoundHttpResponse :
    BaseHttpResponse,
    IRemoveMajorPermanentlyByMajorIdHttpResponse
{
    internal MajorIsNotFoundHttpResponse(RemoveMajorPermanentlyByMajorIdRequest request)
    {
        HttpCode = StatusCodes.Status404NotFound;
        AppCode = MajorAppCode.MAJOR_IS_NOT_FOUND;
        ErrorMessages =
        [
            $"Major with Id = {request.MajorId} is not found."
        ];
    }
}
