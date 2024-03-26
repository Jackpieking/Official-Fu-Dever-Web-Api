using FuDever.Application.Features.Platform.GetAllPlatforms;
using System;
using System.Collections.Generic;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Platform.GetAllPlatforms.Others;

/// <summary>
///     Http response manager for get all platforms feature.
/// </summary>
internal sealed class GetAllPlatformsHttpResponseManager
{
    private readonly Dictionary<
        GetAllPlatformsResponseStatusCode,
        Func<
            GetAllPlatformsRequest,
            GetAllPlatformsResponse,
            IGetAllPlatformsHttpResponse>>
                _dictionary;

    internal GetAllPlatformsHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllPlatformsResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: GetAllPlatformsResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) => new OperationSuccessHttpResponse(response: response));
    }

    internal Func<
        GetAllPlatformsRequest,
        GetAllPlatformsResponse,
        IGetAllPlatformsHttpResponse>
            Resolve(GetAllPlatformsResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}
