using FuDever.Application.Features.Hobby.RestoreHobbyByHobbyId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.HttpResponseMapper.Entities.Hobby.RestoreHobbyByHobbyId.Others;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Hobby.RestoreHobbyByHobbyId;

/// <summary>
///     Restore hobby by hobby
///     Id response status code - hobby id not
///     found http response.
/// </summary>
internal sealed class HobbyIsNotTemporarilyRemovedHttpResponse :
    BaseHttpResponse,
    IRestoreHobbyByHobbyIdHttpResponse
{
    internal HobbyIsNotTemporarilyRemovedHttpResponse(
        RestoreHobbyByHobbyIdRequest request)
    {
        HttpCode = StatusCodes.Status400BadRequest;
        AppCode = HobbyAppCode.HOBBY_IS_NOT_TEMPORARILY_REMOVED;
        ErrorMessages =
        [
            $"Hobby with Id = {request.HobbyId} is not found in temporarily removed storage."
        ];
    }
}
