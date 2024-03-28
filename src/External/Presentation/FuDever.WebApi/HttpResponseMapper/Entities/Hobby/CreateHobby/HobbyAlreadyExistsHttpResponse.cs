using FuDever.Application.Features.Hobby.CreateHobby;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.HttpResponseMapper.Entities.Hobby.CreateHobby.Others;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Hobby.CreateHobby;

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
