using System.Collections.Generic;
using System;
using FuDever.Application.Features.Platform.UpdatePlatformByPlatformId;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Platform.UpdatePlatformByPlatformId.Others;

/// <summary>
///     Http response manager for update platform
///     by platform id feature.
/// </summary>
internal sealed class UpdatePlatformByPlatformIdHttpResponseManager
{
    private readonly Dictionary<
    UpdatePlatformByPlatformIdResponseStatusCode,
    Func<
        UpdatePlatformByPlatformIdRequest,
        UpdatePlatformByPlatformIdResponse,
        IUpdatePlatformByPlatformIdHttpResponse>>
            _dictionary;

    internal UpdatePlatformByPlatformIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: UpdatePlatformByPlatformIdResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: UpdatePlatformByPlatformIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, _) => new OperationSuccessHttpResponse());

        _dictionary.Add(
            key: UpdatePlatformByPlatformIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, _) => new DatabaseOperationFailHttpResponse());

        _dictionary.Add(
            key: UpdatePlatformByPlatformIdResponseStatusCode.PLATFORM_IS_ALREADY_TEMPORARILY_REMOVED,
            value: (request, _) => new PlatformIsAlreadyTemporarilyRemovedHttpResponse(request: request));

        _dictionary.Add(
            key: UpdatePlatformByPlatformIdResponseStatusCode.PLATFORM_ALREADY_EXISTS,
            value: (request, _) => new PlatformAlreadyExistsHttpResponse(request: request));

        _dictionary.Add(
            key: UpdatePlatformByPlatformIdResponseStatusCode.PLATFORM_IS_NOT_FOUND,
            value: (request, _) => new PlatformIsNotFoundHttpResponse(request: request));
    }

    internal Func<
        UpdatePlatformByPlatformIdRequest,
        UpdatePlatformByPlatformIdResponse,
        IUpdatePlatformByPlatformIdHttpResponse>
            Resolve(UpdatePlatformByPlatformIdResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}
