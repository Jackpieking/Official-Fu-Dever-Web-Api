using System.Collections.Generic;
using System;
using FuDever.Application.Features.Hobby.RemoveHobbyTemporarilyByHobbyId;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Hobby.RemoveHobbyTemporarilyByHobbyId.Others;

/// <summary>
///     Http response manager for remove hobby
///     temporarily by hobby id feature.
/// </summary>
internal sealed class RemoveHobbyTemporarilyByHobbyIdHttpResponseManager
{
    private readonly Dictionary<
        RemoveHobbyTemporarilyByHobbyIdResponseStatusCode,
        Func<
            RemoveHobbyTemporarilyByHobbyIdRequest,
            RemoveHobbyTemporarilyByHobbyIdResponse,
            IRemoveHobbyTemporarilyByHobbyIdHttpResponse>>
                _dictionary;

    internal RemoveHobbyTemporarilyByHobbyIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RemoveHobbyTemporarilyByHobbyIdResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: RemoveHobbyTemporarilyByHobbyIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, _) => new OperationSuccessHttpResponse());

        _dictionary.Add(
            key: RemoveHobbyTemporarilyByHobbyIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, _) => new DatabaseOperationFailHttpResponse());

        _dictionary.Add(
            key: RemoveHobbyTemporarilyByHobbyIdResponseStatusCode.HOBBY_IS_NOT_FOUND,
            value: (request, _) => new HobbyIsNotFoundHttpResponse(request: request));

        _dictionary.Add(
            key: RemoveHobbyTemporarilyByHobbyIdResponseStatusCode.HOBBY_IS_ALREADY_TEMPORARILY_REMOVED,
            value: (request, _) => new HobbyIsAlreadyTemporarilyRemovedHttpResponse(request: request));
    }

    internal Func<
        RemoveHobbyTemporarilyByHobbyIdRequest,
        RemoveHobbyTemporarilyByHobbyIdResponse,
        IRemoveHobbyTemporarilyByHobbyIdHttpResponse>
            Resolve(RemoveHobbyTemporarilyByHobbyIdResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}
