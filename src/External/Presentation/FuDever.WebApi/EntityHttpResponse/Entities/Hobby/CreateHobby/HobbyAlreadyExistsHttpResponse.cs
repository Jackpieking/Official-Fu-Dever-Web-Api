using FuDever.Application.Features.Hobby.CreateHobby;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Hobby.CreateHobby.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Hobby.CreateHobby;

/// <summary>
///     Create hobby response status code
///     - hobby already exists http response.
/// </summary>
internal sealed class HobbyAlreadyExistsHttpResponse :
    BaseHttpResponse,
    ICreateHobbyHttpResponse
{
    internal HobbyAlreadyExistsHttpResponse(CreateHobbyRequest request)
    {
        HttpCode = StatusCodes.Status409Conflict;
        AppCode = HobbyAppCode.HOBBY_ALREADY_EXISTS;
        ErrorMessages =
        [
            $"Hobby with name = {request.NewHobbyName} already exists."
        ];
    }
}
