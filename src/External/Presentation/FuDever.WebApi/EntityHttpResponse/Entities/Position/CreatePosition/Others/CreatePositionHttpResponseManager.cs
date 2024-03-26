using FuDever.Application.Features.Position.CreatePosition;
using System;
using System.Collections.Generic;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Position.CreatePosition.Others;

/// <summary>
///     Http response manager for create position feature.
/// </summary>
internal sealed class CreatePositionHttpResponseManager
{
    private readonly Dictionary<
        CreatePositionResponseStatusCode,
        Func<
            CreatePositionRequest,
            CreatePositionResponse,
            ICreatePositionHttpResponse>>
                _dictionary;

    internal CreatePositionHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: CreatePositionResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: CreatePositionResponseStatusCode.OPERATION_SUCCESS,
            value: (_, _) => new OperationSuccessHttpResponse());

        _dictionary.Add(
            key: CreatePositionResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, _) => new DatabaseOperationFailHttpResponse());

        _dictionary.Add(
            key: CreatePositionResponseStatusCode.POSITION_IS_ALREADY_TEMPORARILY_REMOVED,
            value: (request, _) => new PositionIsAlreadyTemporarilyRemovedHttpResponse(request: request));

        _dictionary.Add(
            key: CreatePositionResponseStatusCode.POSITION_ALREADY_EXISTS,
            value: (request, _) => new PositionAlreadyExistsHttpResponse(request: request));
    }

    internal Func<
        CreatePositionRequest,
        CreatePositionResponse,
        ICreatePositionHttpResponse>
            Resolve(CreatePositionResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}
