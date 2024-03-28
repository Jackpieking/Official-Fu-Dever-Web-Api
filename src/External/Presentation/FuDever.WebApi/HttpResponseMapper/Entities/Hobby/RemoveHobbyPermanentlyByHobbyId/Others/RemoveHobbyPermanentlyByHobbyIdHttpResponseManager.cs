using System.Collections.Generic;
using System;
using FuDever.Application.Features.Hobby.RemoveHobbyPermanentlyByHobbyId;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Hobby.RemoveHobbyPermanentlyByHobbyId.Others;

/// <summary>
///     Http response manager for remove hobby
///     permanently by hobby id feature.
/// </summary>
internal sealed class RemoveHobbyPermanentlyByHobbyIdHttpResponseManager
{
    private readonly Dictionary<
        RemoveHobbyPermanentlyByHobbyIdResponseStatusCode,
        Func<
            RemoveHobbyPermanentlyByHobbyIdRequest,
            RemoveHobbyPermanentlyByHobbyIdResponse,
            IRemoveHobbyPermanentlyByHobbyIdHttpResponse>>
                _dictionary;

    internal RemoveHobbyPermanentlyByHobbyIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RemoveHobbyPermanentlyByHobbyIdResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: RemoveHobbyPermanentlyByHobbyIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, _) => new OperationSuccessHttpResponse());

        _dictionary.Add(
            key: RemoveHobbyPermanentlyByHobbyIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, _) => new DatabaseOperationFailHttpResponse());

        _dictionary.Add(
            key: RemoveHobbyPermanentlyByHobbyIdResponseStatusCode.HOBBY_IS_NOT_FOUND,
            value: (request, _) => new HobbyIsNotFoundHttpResponse(request: request));

        _dictionary.Add(
            key: RemoveHobbyPermanentlyByHobbyIdResponseStatusCode.HOBBY_IS_NOT_TEMPORARILY_REMOVED,
            value: (request, _) => new HobbyIsNotTemporarilyRemovedHttpResponse(request: request));
    }

    internal Func<
        RemoveHobbyPermanentlyByHobbyIdRequest,
        RemoveHobbyPermanentlyByHobbyIdResponse,
        IRemoveHobbyPermanentlyByHobbyIdHttpResponse>
            Resolve(RemoveHobbyPermanentlyByHobbyIdResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}
