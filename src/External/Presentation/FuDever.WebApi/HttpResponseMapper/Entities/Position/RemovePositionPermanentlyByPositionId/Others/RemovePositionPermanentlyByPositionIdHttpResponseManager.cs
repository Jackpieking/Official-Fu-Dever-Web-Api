using System.Collections.Generic;
using System;
using FuDever.Application.Features.Position.RemovePositionPermanentlyByPositionId;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Position.RemovePositionPermanentlyByPositionId.Others;

/// <summary>
///     Http response manager for remove position
///     permanently by position id feature.
/// </summary>
internal sealed class RemovePositionPermanentlyByPositionIdHttpResponseManager
{
    private readonly Dictionary<
        RemovePositionPermanentlyByPositionIdResponseStatusCode,
        Func<
            RemovePositionPermanentlyByPositionIdRequest,
            RemovePositionPermanentlyByPositionIdResponse,
            IRemovePositionPermanentlyByPositionIdHttpResponse>>
                _dictionary;

    internal RemovePositionPermanentlyByPositionIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RemovePositionPermanentlyByPositionIdResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: RemovePositionPermanentlyByPositionIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, _) => new OperationSuccessHttpResponse());

        _dictionary.Add(
            key: RemovePositionPermanentlyByPositionIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, _) => new DatabaseOperationFailHttpResponse());

        _dictionary.Add(
            key: RemovePositionPermanentlyByPositionIdResponseStatusCode.POSITION_IS_NOT_FOUND,
            value: (request, _) => new PositionIsNotFoundHttpResponse(request: request));

        _dictionary.Add(
            key: RemovePositionPermanentlyByPositionIdResponseStatusCode.POSITION_IS_NOT_TEMPORARILY_REMOVED,
            value: (request, _) => new PositionIsNotTemporarilyRemovedHttpResponse(request: request));
    }

    internal Func<
        RemovePositionPermanentlyByPositionIdRequest,
        RemovePositionPermanentlyByPositionIdResponse,
        IRemovePositionPermanentlyByPositionIdHttpResponse>
            Resolve(RemovePositionPermanentlyByPositionIdResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}
