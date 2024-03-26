using FuDever.Application.Features.Hobby.RemoveHobbyTemporarilyByHobbyId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Hobby.RemoveHobbyTemporarilyByHobbyId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Hobby.RemoveHobbyTemporarilyByHobbyId;

/// <summary>
///     Remove hobby temporarily by hobby
///     Id response status code - hobby is already
///     temporarily removed http response.
/// </summary>
internal sealed class HobbyIsAlreadyTemporarilyRemovedHttpResponse :
    BaseHttpResponse,
    IRemoveHobbyTemporarilyByHobbyIdHttpResponse
{
    internal HobbyIsAlreadyTemporarilyRemovedHttpResponse(RemoveHobbyTemporarilyByHobbyIdRequest request)
    {
        HttpCode = StatusCodes.Status400BadRequest;
        AppCode = HobbyAppCode.HOBBY_IS_ALREADY_TEMPORARILY_REMOVED;
        ErrorMessages =
        [
            $"Found hobby with Id = {request.HobbyId} in temporarily removed storage."
        ];
    }
}
