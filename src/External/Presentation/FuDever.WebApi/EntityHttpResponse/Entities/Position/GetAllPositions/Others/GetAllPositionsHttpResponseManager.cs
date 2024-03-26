using FuDever.Application.Features.Position.GetAllPositions;
using System;
using System.Collections.Generic;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Position.GetAllPositions.Others;

/// <summary>
///     Http response manager for get all positions feature.
/// </summary>
internal sealed class GetAllPositionsHttpResponseManager
{
    private readonly Dictionary<
        GetAllPositionsResponseStatusCode,
        Func<
            GetAllPositionsRequest,
            GetAllPositionsResponse,
            IGetAllPositionsHttpResponse>>
                _dictionary;

    internal GetAllPositionsHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllPositionsResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: GetAllPositionsResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) => new OperationSuccessHttpResponse(response: response));
    }

    internal Func<
        GetAllPositionsRequest,
        GetAllPositionsResponse,
        IGetAllPositionsHttpResponse>
            Resolve(GetAllPositionsResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}
