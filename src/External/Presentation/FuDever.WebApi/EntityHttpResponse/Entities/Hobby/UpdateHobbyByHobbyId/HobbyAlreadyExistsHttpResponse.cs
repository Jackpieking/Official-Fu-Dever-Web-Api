using FuDever.Application.Features.Hobby.UpdateHobbyByHobbyId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Hobby.UpdateHobbyByHobbyId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Hobby.UpdateHobbyByHobbyId;

/// <summary>
///     Update hobby by hobby id response
///     status code - hobby already exists
///     http response.
/// </summary>
internal sealed class HobbyAlreadyExistsHttpResponse :
    BaseHttpResponse,
    IUpdateHobbyByHobbyIdHttpResponse
{
    internal HobbyAlreadyExistsHttpResponse(UpdateHobbyByHobbyIdRequest request)
    {
        HttpCode = StatusCodes.Status409Conflict;
        AppCode = HobbyAppCode.HOBBY_ALREADY_EXISTS;
        ErrorMessages =
        [
            $"Hobby with name = {request.NewHobbyName} already exists."
        ];
    }
}
