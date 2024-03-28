using System.Collections.Generic;
using System;
using FuDever.Application.Features.Platform.RemovePlatformTemporarilyByPlatformId;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Platform.RemovePlatformTemporarilyByPlatformId.Others;

/// <summary>
///     Http response manager for remove platform
///     temporarily by platform id feature.
/// </summary>
internal sealed class RemovePlatformTemporarilyByPlatformIdHttpResponseManager
{
    private readonly Dictionary<
        RemovePlatformTemporarilyByPlatformIdResponseStatusCode,
        Func<
            RemovePlatformTemporarilyByPlatformIdRequest,
            RemovePlatformTemporarilyByPlatformIdResponse,
            IRemovePlatformTemporarilyByPlatformIdHttpResponse>>
                _dictionary;

    internal RemovePlatformTemporarilyByPlatformIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RemovePlatformTemporarilyByPlatformIdResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: RemovePlatformTemporarilyByPlatformIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, _) => new OperationSuccessHttpResponse());

        _dictionary.Add(
            key: RemovePlatformTemporarilyByPlatformIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, _) => new DatabaseOperationFailHttpResponse());

        _dictionary.Add(
            key: RemovePlatformTemporarilyByPlatformIdResponseStatusCode.PLATFORM_IS_NOT_FOUND,
            value: (request, _) => new PlatformIsNotFoundHttpResponse(request: request));

        _dictionary.Add(
            key: RemovePlatformTemporarilyByPlatformIdResponseStatusCode.PLATFORM_IS_ALREADY_TEMPORARILY_REMOVED,
            value: (request, _) => new PlatformIsAlreadyTemporarilyRemovedHttpResponse(request: request));
    }

    internal Func<
        RemovePlatformTemporarilyByPlatformIdRequest,
        RemovePlatformTemporarilyByPlatformIdResponse,
        IRemovePlatformTemporarilyByPlatformIdHttpResponse>
            Resolve(RemovePlatformTemporarilyByPlatformIdResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}
