using FuDever.Application.Features.Hobby.RemoveHobbyPermanentlyByHobbyId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.HttpResponseMapper.Entities.Hobby.RemoveHobbyPermanentlyByHobbyId.Others;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Hobby.RemoveHobbyPermanentlyByHobbyId;

/// <summary>
///     Remove hobby permanently by hobby
///     Id response status code - hobby id not
///     found http response.
/// </summary>
internal sealed class HobbyIsNotTemporarilyRemovedHttpResponse :
    BaseHttpResponse,
    IRemoveHobbyPermanentlyByHobbyIdHttpResponse
{
    internal HobbyIsNotTemporarilyRemovedHttpResponse(RemoveHobbyPermanentlyByHobbyIdRequest request)
    {
        HttpCode = StatusCodes.Status400BadRequest;
        AppCode = HobbyAppCode.HOBBY_IS_NOT_TEMPORARILY_REMOVED;
        ErrorMessages =
        [
            $"Hobby with Id = {request.HobbyId} is not found in temporarily removed storage."
        ];
    }
}
