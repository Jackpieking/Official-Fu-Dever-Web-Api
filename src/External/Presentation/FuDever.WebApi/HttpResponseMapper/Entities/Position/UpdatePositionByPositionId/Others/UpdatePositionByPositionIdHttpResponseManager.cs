using System.Collections.Generic;
using System;
using FuDever.Application.Features.Position.UpdatePositionByPositionId;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Position.UpdatePositionByPositionId.Others;

/// <summary>
///     Http response manager for update position
///     by position id feature.
/// </summary>
internal sealed class UpdatePositionByPositionIdHttpResponseManager
{
    private readonly Dictionary<
    UpdatePositionByPositionIdResponseStatusCode,
    Func<
        UpdatePositionByPositionIdRequest,
        UpdatePositionByPositionIdResponse,
        IUpdatePositionByPositionIdHttpResponse>>
            _dictionary;

    internal UpdatePositionByPositionIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: UpdatePositionByPositionIdResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: UpdatePositionByPositionIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, _) => new OperationSuccessHttpResponse());

        _dictionary.Add(
            key: UpdatePositionByPositionIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, _) => new DatabaseOperationFailHttpResponse());

        _dictionary.Add(
            key: UpdatePositionByPositionIdResponseStatusCode.POSITION_IS_ALREADY_TEMPORARILY_REMOVED,
            value: (request, _) => new PositionIsAlreadyTemporarilyRemovedHttpResponse(request: request));

        _dictionary.Add(
            key: UpdatePositionByPositionIdResponseStatusCode.POSITION_ALREADY_EXISTS,
            value: (request, _) => new PositionAlreadyExistsHttpResponse(request: request));

        _dictionary.Add(
            key: UpdatePositionByPositionIdResponseStatusCode.POSITION_IS_NOT_FOUND,
            value: (request, _) => new PositionIsNotFoundHttpResponse(request: request));
    }

    internal Func<
        UpdatePositionByPositionIdRequest,
        UpdatePositionByPositionIdResponse,
        IUpdatePositionByPositionIdHttpResponse>
            Resolve(UpdatePositionByPositionIdResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}
