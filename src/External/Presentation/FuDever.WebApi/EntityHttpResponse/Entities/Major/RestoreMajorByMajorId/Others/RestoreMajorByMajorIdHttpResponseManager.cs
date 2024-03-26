using System.Collections.Generic;
using System;
using FuDever.Application.Features.Major.RestoreMajorByMajorId;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Major.RestoreMajorByMajorId.Others;

/// <summary>
///     Http response manager for restore major
///     by major id feature.
/// </summary>
internal sealed class RestoreMajorByMajorIdHttpResponseManager
{
    private readonly Dictionary<
    RestoreMajorByMajorIdResponseStatusCode,
    Func<
        RestoreMajorByMajorIdRequest,
        RestoreMajorByMajorIdResponse,
        IRestoreMajorByMajorIdHttpResponse>>
            _dictionary;

    internal RestoreMajorByMajorIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RestoreMajorByMajorIdResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: RestoreMajorByMajorIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, _) => new OperationSuccessHttpResponse());

        _dictionary.Add(
            key: RestoreMajorByMajorIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, _) => new DatabaseOperationFailHttpResponse());

        _dictionary.Add(
            key: RestoreMajorByMajorIdResponseStatusCode.MAJOR_IS_NOT_FOUND,
            value: (request, _) => new MajorIsNotFoundHttpResponse(request: request));

        _dictionary.Add(
            key: RestoreMajorByMajorIdResponseStatusCode.MAJOR_IS_NOT_TEMPORARILY_REMOVED,
            value: (request, _) => new MajorIsNotTemporarilyRemovedHttpResponse(request: request));
    }

    internal Func<
        RestoreMajorByMajorIdRequest,
        RestoreMajorByMajorIdResponse,
        IRestoreMajorByMajorIdHttpResponse>
            Resolve(RestoreMajorByMajorIdResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}
