using System.Collections.Generic;
using System;
using FuDever.Application.Features.Platform.RemovePlatformPermanentlyByPlatformId;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Platform.RemovePlatformPermanentlyByPlatformId.Others;

/// <summary>
///     Http response manager for remove platform
///     permanently by platform id feature.
/// </summary>
internal sealed class RemovePlatformPermanentlyByPlatformIdHttpResponseManager
{
    private readonly Dictionary<
        RemovePlatformPermanentlyByPlatformIdResponseStatusCode,
        Func<
            RemovePlatformPermanentlyByPlatformIdRequest,
            RemovePlatformPermanentlyByPlatformIdResponse,
            IRemovePlatformPermanentlyByPlatformIdHttpResponse>>
                _dictionary;

    internal RemovePlatformPermanentlyByPlatformIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RemovePlatformPermanentlyByPlatformIdResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: RemovePlatformPermanentlyByPlatformIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, _) => new OperationSuccessHttpResponse());

        _dictionary.Add(
            key: RemovePlatformPermanentlyByPlatformIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, _) => new DatabaseOperationFailHttpResponse());

        _dictionary.Add(
            key: RemovePlatformPermanentlyByPlatformIdResponseStatusCode.PLATFORM_IS_NOT_FOUND,
            value: (request, _) => new PlatformIsNotFoundHttpResponse(request: request));

        _dictionary.Add(
            key: RemovePlatformPermanentlyByPlatformIdResponseStatusCode.PLATFORM_IS_NOT_TEMPORARILY_REMOVED,
            value: (request, _) => new PlatformIsNotTemporarilyRemovedHttpResponse(request: request));
    }

    internal Func<
        RemovePlatformPermanentlyByPlatformIdRequest,
        RemovePlatformPermanentlyByPlatformIdResponse,
        IRemovePlatformPermanentlyByPlatformIdHttpResponse>
            Resolve(RemovePlatformPermanentlyByPlatformIdResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}
