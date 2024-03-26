using FuDever.Application.Features.Hobby.CreateHobby;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Hobby.CreateHobby.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Hobby.CreateHobby;

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
