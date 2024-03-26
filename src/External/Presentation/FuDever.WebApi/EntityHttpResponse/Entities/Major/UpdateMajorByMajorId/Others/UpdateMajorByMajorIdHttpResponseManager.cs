using System.Collections.Generic;
using System;
using FuDever.Application.Features.Major.UpdateMajorByMajorId;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Major.UpdateMajorByMajorId.Others;

/// <summary>
///     Http response manager for update major
///     by major id feature.
/// </summary>
internal sealed class UpdateMajorByMajorIdHttpResponseManager
{
    private readonly Dictionary<
    UpdateMajorByMajorIdResponseStatusCode,
    Func<
        UpdateMajorByMajorIdRequest,
        UpdateMajorByMajorIdResponse,
        IUpdateMajorByMajorIdHttpResponse>>
            _dictionary;

    internal UpdateMajorByMajorIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: UpdateMajorByMajorIdResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: UpdateMajorByMajorIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, _) => new OperationSuccessHttpResponse());

        _dictionary.Add(
            key: UpdateMajorByMajorIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, _) => new DatabaseOperationFailHttpResponse());

        _dictionary.Add(
            key: UpdateMajorByMajorIdResponseStatusCode.MAJOR_IS_ALREADY_TEMPORARILY_REMOVED,
            value: (request, _) => new MajorIsAlreadyTemporarilyRemovedHttpResponse(request: request));

        _dictionary.Add(
            key: UpdateMajorByMajorIdResponseStatusCode.MAJOR_ALREADY_EXISTS,
            value: (request, _) => new MajorAlreadyExistsHttpResponse(request: request));

        _dictionary.Add(
            key: UpdateMajorByMajorIdResponseStatusCode.MAJOR_IS_NOT_FOUND,
            value: (request, _) => new MajorIsNotFoundHttpResponse(request: request));
    }

    internal Func<
        UpdateMajorByMajorIdRequest,
        UpdateMajorByMajorIdResponse,
        IUpdateMajorByMajorIdHttpResponse>
            Resolve(UpdateMajorByMajorIdResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}
