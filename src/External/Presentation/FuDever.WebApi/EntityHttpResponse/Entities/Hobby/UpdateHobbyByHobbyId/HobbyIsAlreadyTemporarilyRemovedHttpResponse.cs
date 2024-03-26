﻿using FuDever.Application.Features.Hobby.UpdateHobbyByHobbyId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Hobby.UpdateHobbyByHobbyId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Hobby.UpdateHobbyByHobbyId;

/// <summary>
///     Update hobby by hobby
///     Id response status code - hobby is already
///     temporarily removed http response.
/// </summary>
internal sealed class HobbyIsAlreadyTemporarilyRemovedHttpResponse :
    BaseHttpResponse,
    IUpdateHobbyByHobbyIdHttpResponse
{
    internal HobbyIsAlreadyTemporarilyRemovedHttpResponse(UpdateHobbyByHobbyIdRequest request)
    {
        HttpCode = StatusCodes.Status400BadRequest;
        AppCode = HobbyAppCode.HOBBY_IS_ALREADY_TEMPORARILY_REMOVED;
        ErrorMessages =
        [
            $"Found hobby with Id = {request.HobbyId} in temporarily removed storage."
        ];
    }
}
