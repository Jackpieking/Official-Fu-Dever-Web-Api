using FuDever.Application.Features.Hobby.CreateHobby;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.HttpResponseMapper.Entities.Hobby.CreateHobby.Others;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Hobby.CreateHobby;

/// <summary>
///     Create hobby response status code
///     - hobby is already temporarily removed
///     http response.
/// </summary>
internal sealed class HobbyIsAlreadyTemporarilyRemovedHttpResponse :
    BaseHttpResponse,
    ICreateHobbyHttpResponse
{
    internal HobbyIsAlreadyTemporarilyRemovedHttpResponse(CreateHobbyRequest request)
    {
        HttpCode = StatusCodes.Status400BadRequest;
        AppCode = HobbyAppCode.HOBBY_IS_ALREADY_TEMPORARILY_REMOVED;
        ErrorMessages =
        [
            $"Found hobby with name = {request.NewHobbyName} in temporarily removed storage."
        ];
    }
}
