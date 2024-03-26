using System.Collections.Generic;
using System;
using FuDever.Application.Features.Hobby.UpdateHobbyByHobbyId;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Hobby.UpdateHobbyByHobbyId.Others;

/// <summary>
///     Http response manager for update hobby
///     by hobby id feature.
/// </summary>
internal sealed class UpdateHobbyByHobbyIdHttpResponseManager
{
    private readonly Dictionary<
    UpdateHobbyByHobbyIdResponseStatusCode,
    Func<
        UpdateHobbyByHobbyIdRequest,
        UpdateHobbyByHobbyIdResponse,
        IUpdateHobbyByHobbyIdHttpResponse>>
            _dictionary;

    internal UpdateHobbyByHobbyIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: UpdateHobbyByHobbyIdResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: UpdateHobbyByHobbyIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, _) => new OperationSuccessHttpResponse());

        _dictionary.Add(
            key: UpdateHobbyByHobbyIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, _) => new DatabaseOperationFailHttpResponse());

        _dictionary.Add(
            key: UpdateHobbyByHobbyIdResponseStatusCode.HOBBY_IS_ALREADY_TEMPORARILY_REMOVED,
            value: (request, _) => new HobbyIsAlreadyTemporarilyRemovedHttpResponse(request: request));

        _dictionary.Add(
            key: UpdateHobbyByHobbyIdResponseStatusCode.HOBBY_ALREADY_EXISTS,
            value: (request, _) => new HobbyAlreadyExistsHttpResponse(request: request));

        _dictionary.Add(
            key: UpdateHobbyByHobbyIdResponseStatusCode.HOBBY_IS_NOT_FOUND,
            value: (request, _) => new HobbyIsNotFoundHttpResponse(request: request));
    }

    internal Func<
        UpdateHobbyByHobbyIdRequest,
        UpdateHobbyByHobbyIdResponse,
        IUpdateHobbyByHobbyIdHttpResponse>
            Resolve(UpdateHobbyByHobbyIdResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}
