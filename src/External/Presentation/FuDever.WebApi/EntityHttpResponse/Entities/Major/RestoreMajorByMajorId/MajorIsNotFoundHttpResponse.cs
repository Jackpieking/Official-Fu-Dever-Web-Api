using FuDever.Application.Features.Major.RestoreMajorByMajorId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Major.RestoreMajorByMajorId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Major.RestoreMajorByMajorId;

/// <summary>
///     Restore major by major
///     Id response status code - major is not
///     found http response.
/// </summary>
internal sealed class MajorIsNotFoundHttpResponse :
    BaseHttpResponse,
    IRestoreMajorByMajorIdHttpResponse
{
    internal MajorIsNotFoundHttpResponse(RestoreMajorByMajorIdRequest request)
    {
        HttpCode = StatusCodes.Status404NotFound;
        AppCode = MajorAppCode.MAJOR_IS_NOT_FOUND;
        ErrorMessages =
        [
            $"Major with Id = {request.MajorId} is not found."
        ];
    }
}
