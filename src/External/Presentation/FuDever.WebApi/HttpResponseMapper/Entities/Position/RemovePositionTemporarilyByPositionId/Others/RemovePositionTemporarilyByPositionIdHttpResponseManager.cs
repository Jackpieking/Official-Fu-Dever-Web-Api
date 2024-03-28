using System.Collections.Generic;
using System;
using FuDever.Application.Features.Position.RemovePositionTemporarilyByPositionId;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Position.RemovePositionTemporarilyByPositionId.Others;

/// <summary>
///     Http response manager for remove position
///     temporarily by position id feature.
/// </summary>
internal sealed class RemovePositionTemporarilyByPositionIdHttpResponseManager
{
    private readonly Dictionary<
        RemovePositionTemporarilyByPositionIdResponseStatusCode,
        Func<
            RemovePositionTemporarilyByPositionIdRequest,
            RemovePositionTemporarilyByPositionIdResponse,
            IRemovePositionTemporarilyByPositionIdHttpResponse>>
                _dictionary;

    internal RemovePositionTemporarilyByPositionIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RemovePositionTemporarilyByPositionIdResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: RemovePositionTemporarilyByPositionIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, _) => new OperationSuccessHttpResponse());

        _dictionary.Add(
            key: RemovePositionTemporarilyByPositionIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, _) => new DatabaseOperationFailHttpResponse());

        _dictionary.Add(
            key: RemovePositionTemporarilyByPositionIdResponseStatusCode.POSITION_IS_NOT_FOUND,
            value: (request, _) => new PositionIsNotFoundHttpResponse(request: request));

        _dictionary.Add(
            key: RemovePositionTemporarilyByPositionIdResponseStatusCode.POSITION_IS_ALREADY_TEMPORARILY_REMOVED,
            value: (request, _) => new PositionIsAlreadyTemporarilyRemovedHttpResponse(request: request));
    }

    internal Func<
        RemovePositionTemporarilyByPositionIdRequest,
        RemovePositionTemporarilyByPositionIdResponse,
        IRemovePositionTemporarilyByPositionIdHttpResponse>
            Resolve(RemovePositionTemporarilyByPositionIdResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}
