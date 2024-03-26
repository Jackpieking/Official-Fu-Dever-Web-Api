﻿using FuDever.Application.Features.Position.RemovePositionPermanentlyByPositionId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Position.RemovePositionPermanentlyByPositionId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Position.RemovePositionPermanentlyByPositionId;

/// <summary>
///     Remove position permanently by position
///     Id response status code - position is not
///     found http response.
/// </summary>
internal sealed class PositionIsNotFoundHttpResponse :
    BaseHttpResponse,
    IRemovePositionPermanentlyByPositionIdHttpResponse
{
    internal PositionIsNotFoundHttpResponse(RemovePositionPermanentlyByPositionIdRequest request)
    {
        HttpCode = StatusCodes.Status404NotFound;
        AppCode = PositionAppCode.POSITION_IS_NOT_FOUND;
        ErrorMessages =
        [
            $"Position with Id = {request.PositionId} is not found."
        ];
    }
}
