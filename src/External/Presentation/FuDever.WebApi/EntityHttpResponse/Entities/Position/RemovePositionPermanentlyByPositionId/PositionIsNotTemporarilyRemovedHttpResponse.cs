using FuDever.Application.Features.Position.RemovePositionPermanentlyByPositionId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Position.RemovePositionPermanentlyByPositionId.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Position.RemovePositionPermanentlyByPositionId;

/// <summary>
///     Remove position permanently by position
///     Id response status code - position id not
///     found http response.
/// </summary>
internal sealed class PositionIsNotTemporarilyRemovedHttpResponse :
    BaseHttpResponse,
    IRemovePositionPermanentlyByPositionIdHttpResponse
{
    internal PositionIsNotTemporarilyRemovedHttpResponse(RemovePositionPermanentlyByPositionIdRequest request)
    {
        HttpCode = StatusCodes.Status400BadRequest;
        AppCode = PositionAppCode.POSITION_IS_ALREADY_TEMPORARILY_REMOVED;
        ErrorMessages =
        [
            $"Position with Id = {request.PositionId} is not found in temporarily removed storage."
        ];
    }
}
