using System.Collections.Generic;
using System;
using FuDever.Application.Features.Platform.GetAllTemporarilyRemovedPlatforms;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Platform.GetAllTemporarilyRemovedPlatforms.Others;

/// <summary>
///     Http response manager for get all
///     temporarily removed platforms feature.
/// </summary>
internal sealed class GetAllTemporarilyRemovedPlatformsHttpResponseManager
{
    private readonly Dictionary<
        GetAllTemporarilyRemovedPlatformsResponseStatusCode,
        Func<
            GetAllTemporarilyRemovedPlatformsRequest,
            GetAllTemporarilyRemovedPlatformsResponse,
            IGetAllTemporarilyRemovedPlatformsHttpResponse>>
                _dictionary;

    internal GetAllTemporarilyRemovedPlatformsHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllTemporarilyRemovedPlatformsResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: GetAllTemporarilyRemovedPlatformsResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) => new OperationSuccessHttpResponse(response: response));
    }

    internal Func<
        GetAllTemporarilyRemovedPlatformsRequest,
        GetAllTemporarilyRemovedPlatformsResponse,
        IGetAllTemporarilyRemovedPlatformsHttpResponse>
            Resolve(GetAllTemporarilyRemovedPlatformsResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}