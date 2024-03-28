using FuDever.Application.Features.Position.RemovePositionTemporarilyByPositionId;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.HttpResponseMapper.Entities.Position.RemovePositionTemporarilyByPositionId.Others;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Position.RemovePositionTemporarilyByPositionId;

/// <summary>
///     Remove position temporarily by position
///     Id response status code - position is already
///     temporarily removed http response.
/// </summary>
internal sealed class PositionIsAlreadyTemporarilyRemovedHttpResponse :
    BaseHttpResponse,
    IRemovePositionTemporarilyByPositionIdHttpResponse
{
    internal PositionIsAlreadyTemporarilyRemovedHttpResponse(RemovePositionTemporarilyByPositionIdRequest request)
    {
        HttpCode = StatusCodes.Status400BadRequest;
        AppCode = PositionAppCode.POSITION_IS_ALREADY_TEMPORARILY_REMOVED;
        ErrorMessages =
        [
            $"Found position with Id = {request.PositionId} in temporarily removed storage."
        ];
    }
}
