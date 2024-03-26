using FuDever.Application.Features.Hobby.UpdateHobbyByHobbyId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Hobby.UpdateHobbyByHobbyId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Hobby.UpdateHobbyByHobbyId;

/// <summary>
///     Update hobby by hobby
///     Id response status code - hobby is not
///     found http response.
/// </summary>
internal sealed class HobbyIsNotFoundHttpResponse :
    BaseHttpResponse,
    IUpdateHobbyByHobbyIdHttpResponse
{
    internal HobbyIsNotFoundHttpResponse(UpdateHobbyByHobbyIdRequest request)
    {
        HttpCode = StatusCodes.Status404NotFound;
        AppCode = HobbyAppCode.HOBBY_IS_NOT_FOUND;
        ErrorMessages =
        [
            $"Hobby with Id = {request.HobbyId} is not found."
        ];
    }
}
