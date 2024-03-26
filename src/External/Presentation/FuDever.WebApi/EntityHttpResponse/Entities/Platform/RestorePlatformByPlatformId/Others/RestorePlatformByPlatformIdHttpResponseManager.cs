using System.Collections.Generic;
using System;
using FuDever.Application.Features.Platform.RestorePlatformByPlatformId;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Platform.RestorePlatformByPlatformId.Others;

/// <summary>
///     Http response manager for restore platform
///     by platform id feature.
/// </summary>
internal sealed class RestorePlatformByPlatformIdHttpResponseManager
{
    private readonly Dictionary<
    RestorePlatformByPlatformIdResponseStatusCode,
    Func<
        RestorePlatformByPlatformIdRequest,
        RestorePlatformByPlatformIdResponse,
        IRestorePlatformByPlatformIdHttpResponse>>
            _dictionary;

    internal RestorePlatformByPlatformIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RestorePlatformByPlatformIdResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: RestorePlatformByPlatformIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, _) => new OperationSuccessHttpResponse());

        _dictionary.Add(
            key: RestorePlatformByPlatformIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, _) => new DatabaseOperationFailHttpResponse());

        _dictionary.Add(
            key: RestorePlatformByPlatformIdResponseStatusCode.PLATFORM_IS_NOT_FOUND,
            value: (request, _) => new PlatformIsNotFoundHttpResponse(request: request));

        _dictionary.Add(
            key: RestorePlatformByPlatformIdResponseStatusCode.PLATFORM_IS_NOT_TEMPORARILY_REMOVED,
            value: (request, _) => new PlatformIsNotTemporarilyRemovedHttpResponse(request: request));
    }

    internal Func<
        RestorePlatformByPlatformIdRequest,
        RestorePlatformByPlatformIdResponse,
        IRestorePlatformByPlatformIdHttpResponse>
            Resolve(RestorePlatformByPlatformIdResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}
