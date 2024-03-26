using FuDever.Application.Features.Major.UpdateMajorByMajorId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Major.UpdateMajorByMajorId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Major.UpdateMajorByMajorId;

/// <summary>
///     Update major by major
///     Id response status code - major is not
///     found http response.
/// </summary>
internal sealed class MajorIsNotFoundHttpResponse :
    BaseHttpResponse,
    IUpdateMajorByMajorIdHttpResponse
{
    internal MajorIsNotFoundHttpResponse(UpdateMajorByMajorIdRequest request)
    {
        HttpCode = StatusCodes.Status404NotFound;
        AppCode = MajorAppCode.MAJOR_IS_NOT_FOUND;
        ErrorMessages =
        [
            $"Major with Id = {request.MajorId} is not found."
        ];
    }
}
