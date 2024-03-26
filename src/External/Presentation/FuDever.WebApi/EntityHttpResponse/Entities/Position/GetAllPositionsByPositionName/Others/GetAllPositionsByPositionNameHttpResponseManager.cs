using System.Collections.Generic;
using System;
using FuDever.Application.Features.Position.GetAllPositionsByPositionName;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Position.GetAllPositionsByPositionName.Others;

/// <summary>
///     Http response manager for get all positions
///     by position name feature.
/// </summary>
internal sealed class GetAllPositionsByPositionNameHttpResponseManager
{
    private readonly Dictionary<
        GetAllPositionsByPositionNameResponseStatusCode,
        Func<
            GetAllPositionsByPositionNameRequest,
            GetAllPositionsByPositionNameResponse,
            IGetAllPositionsByPositionNameHttpResponse>>
                _dictionary;

    internal GetAllPositionsByPositionNameHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllPositionsByPositionNameResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: GetAllPositionsByPositionNameResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) => new OperationSuccessHttpResponse(response: response));
    }

    internal Func<
        GetAllPositionsByPositionNameRequest,
        GetAllPositionsByPositionNameResponse,
        IGetAllPositionsByPositionNameHttpResponse>
            Resolve(GetAllPositionsByPositionNameResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}
