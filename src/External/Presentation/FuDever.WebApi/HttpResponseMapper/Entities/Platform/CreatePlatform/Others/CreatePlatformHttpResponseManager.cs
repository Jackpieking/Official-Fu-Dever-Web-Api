using FuDever.Application.Features.Platform.CreatePlatform;
using System;
using System.Collections.Generic;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Platform.CreatePlatform.Others;

/// <summary>
///     Http response manager for create platform feature.
/// </summary>
internal sealed class CreatePlatformHttpResponseManager
{
    private readonly Dictionary<
        CreatePlatformResponseStatusCode,
        Func<
            CreatePlatformRequest,
            CreatePlatformResponse,
            ICreatePlatformHttpResponse>>
                _dictionary;

    internal CreatePlatformHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: CreatePlatformResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: CreatePlatformResponseStatusCode.OPERATION_SUCCESS,
            value: (_, _) => new OperationSuccessHttpResponse());

        _dictionary.Add(
            key: CreatePlatformResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, _) => new DatabaseOperationFailHttpResponse());

        _dictionary.Add(
            key: CreatePlatformResponseStatusCode.PLATFORM_IS_ALREADY_TEMPORARILY_REMOVED,
            value: (request, _) => new PlatformIsAlreadyTemporarilyRemovedHttpResponse(request: request));

        _dictionary.Add(
            key: CreatePlatformResponseStatusCode.PLATFORM_ALREADY_EXISTS,
            value: (request, _) => new PlatformAlreadyExistsHttpResponse(request: request));
    }

    internal Func<
        CreatePlatformRequest,
        CreatePlatformResponse,
        ICreatePlatformHttpResponse>
            Resolve(CreatePlatformResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}
