using FuDever.Application.Features.Hobby.RemoveHobbyTemporarilyByHobbyId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Hobby.RemoveHobbyTemporarilyByHobbyId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Hobby.RemoveHobbyTemporarilyByHobbyId;

/// <summary>
///     Remove hobby temporarily by hobby
///     Id response status code - hobby is not
///     found http response.
/// </summary>
internal sealed class HobbyIsNotFoundHttpResponse :
    BaseHttpResponse,
    IRemoveHobbyTemporarilyByHobbyIdHttpResponse
{
    internal HobbyIsNotFoundHttpResponse(RemoveHobbyTemporarilyByHobbyIdRequest request)
    {
        HttpCode = StatusCodes.Status404NotFound;
        AppCode = HobbyAppCode.HOBBY_IS_NOT_FOUND;
        ErrorMessages =
        [
            $"Hobby with Id = {request.HobbyId} is not found."
        ];
    }
}
