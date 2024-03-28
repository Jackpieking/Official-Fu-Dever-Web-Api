using System.Collections.Generic;
using System;
using FuDever.Application.Features.Position.GetAllTemporarilyRemovedPositions;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Position.GetAllTemporarilyRemovedPositions.Others;

/// <summary>
///     Http response manager for get all
///     temporarily removed positions feature.
/// </summary>
internal sealed class GetAllTemporarilyRemovedPositionsHttpResponseManager
{
    private readonly Dictionary<
        GetAllTemporarilyRemovedPositionsResponseStatusCode,
        Func<
            GetAllTemporarilyRemovedPositionsRequest,
            GetAllTemporarilyRemovedPositionsResponse,
            IGetAllTemporarilyRemovedPositionsHttpResponse>>
                _dictionary;

    internal GetAllTemporarilyRemovedPositionsHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllTemporarilyRemovedPositionsResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: GetAllTemporarilyRemovedPositionsResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) => new OperationSuccessHttpResponse(response: response));
    }

    internal Func<
        GetAllTemporarilyRemovedPositionsRequest,
        GetAllTemporarilyRemovedPositionsResponse,
        IGetAllTemporarilyRemovedPositionsHttpResponse>
            Resolve(GetAllTemporarilyRemovedPositionsResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}