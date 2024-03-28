using System.Collections.Generic;
using System;
using FuDever.Application.Features.Hobby.RestoreHobbyByHobbyId;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Hobby.RestoreHobbyByHobbyId.Others;

/// <summary>
///     Http response manager for restore hobby
///     by hobby id feature.
/// </summary>
internal sealed class RestoreHobbyByHobbyIdHttpResponseManager
{
    private readonly Dictionary<
    RestoreHobbyByHobbyIdResponseStatusCode,
    Func<
        RestoreHobbyByHobbyIdRequest,
        RestoreHobbyByHobbyIdResponse,
        IRestoreHobbyByHobbyIdHttpResponse>>
            _dictionary;

    internal RestoreHobbyByHobbyIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RestoreHobbyByHobbyIdResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: RestoreHobbyByHobbyIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, _) => new OperationSuccessHttpResponse());

        _dictionary.Add(
            key: RestoreHobbyByHobbyIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, _) => new DatabaseOperationFailHttpResponse());

        _dictionary.Add(
            key: RestoreHobbyByHobbyIdResponseStatusCode.HOBBY_IS_NOT_FOUND,
            value: (request, _) => new HobbyIsNotFoundHttpResponse(request: request));

        _dictionary.Add(
            key: RestoreHobbyByHobbyIdResponseStatusCode.HOBBY_IS_NOT_TEMPORARILY_REMOVED,
            value: (request, _) => new HobbyIsNotTemporarilyRemovedHttpResponse(request: request));
    }

    internal Func<
        RestoreHobbyByHobbyIdRequest,
        RestoreHobbyByHobbyIdResponse,
        IRestoreHobbyByHobbyIdHttpResponse>
            Resolve(RestoreHobbyByHobbyIdResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}
