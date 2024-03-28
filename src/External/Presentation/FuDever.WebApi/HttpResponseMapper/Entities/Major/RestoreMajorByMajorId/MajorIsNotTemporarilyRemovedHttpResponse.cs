using FuDever.Application.Features.Major.RestoreMajorByMajorId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.HttpResponseMapper.Entities.Major.RestoreMajorByMajorId.Others;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Major.RestoreMajorByMajorId;

/// <summary>
///     Restore major by major
///     Id response status code - major id not
///     found http response.
/// </summary>
internal sealed class MajorIsNotTemporarilyRemovedHttpResponse :
    BaseHttpResponse,
    IRestoreMajorByMajorIdHttpResponse
{
    internal MajorIsNotTemporarilyRemovedHttpResponse(
        RestoreMajorByMajorIdRequest request)
    {
        HttpCode = StatusCodes.Status400BadRequest;
        AppCode = MajorAppCode.MAJOR_IS_NOT_TEMPORARILY_REMOVED;
        ErrorMessages =
        [
            $"Major with Id = {request.MajorId} is not found in temporarily removed storage."
        ];
    }
}
