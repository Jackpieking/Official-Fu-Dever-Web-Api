using FuDever.Application.Features.Hobby.RestoreHobbyByHobbyId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.HttpResponseMapper.Entities.Hobby.RestoreHobbyByHobbyId.Others;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Hobby.RestoreHobbyByHobbyId;

/// <summary>
///     Restore hobby by hobby
///     Id response status code - hobby is not
///     found http response.
/// </summary>
internal sealed class HobbyIsNotFoundHttpResponse :
    BaseHttpResponse,
    IRestoreHobbyByHobbyIdHttpResponse
{
    internal HobbyIsNotFoundHttpResponse(RestoreHobbyByHobbyIdRequest request)
    {
        HttpCode = StatusCodes.Status404NotFound;
        AppCode = HobbyAppCode.HOBBY_IS_NOT_FOUND;
        ErrorMessages =
        [
            $"Hobby with Id = {request.HobbyId} is not found."
        ];
    }
}
