using System.Collections.Generic;
using System;
using FuDever.Application.Features.Position.RestorePositionByPositionId;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Position.RestorePositionByPositionId.Others;

/// <summary>
///     Http response manager for restore position
///     by position id feature.
/// </summary>
internal sealed class RestorePositionByPositionIdHttpResponseManager
{
    private readonly Dictionary<
    RestorePositionByPositionIdResponseStatusCode,
    Func<
        RestorePositionByPositionIdRequest,
        RestorePositionByPositionIdResponse,
        IRestorePositionByPositionIdHttpResponse>>
            _dictionary;

    internal RestorePositionByPositionIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RestorePositionByPositionIdResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: RestorePositionByPositionIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, _) => new OperationSuccessHttpResponse());

        _dictionary.Add(
            key: RestorePositionByPositionIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, _) => new DatabaseOperationFailHttpResponse());

        _dictionary.Add(
            key: RestorePositionByPositionIdResponseStatusCode.POSITION_IS_NOT_FOUND,
            value: (request, _) => new PositionIsNotFoundHttpResponse(request: request));

        _dictionary.Add(
            key: RestorePositionByPositionIdResponseStatusCode.POSITION_IS_NOT_TEMPORARILY_REMOVED,
            value: (request, _) => new PositionIsNotTemporarilyRemovedHttpResponse(request: request));
    }

    internal Func<
        RestorePositionByPositionIdRequest,
        RestorePositionByPositionIdResponse,
        IRestorePositionByPositionIdHttpResponse>
            Resolve(RestorePositionByPositionIdResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}
