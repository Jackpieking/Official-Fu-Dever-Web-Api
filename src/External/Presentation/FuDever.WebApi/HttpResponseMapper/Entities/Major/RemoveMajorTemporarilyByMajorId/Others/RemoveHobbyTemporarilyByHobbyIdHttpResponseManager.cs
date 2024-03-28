using System.Collections.Generic;
using System;
using FuDever.Application.Features.Major.RemoveMajorTemporarilyByMajorId;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Major.RemoveMajorTemporarilyByMajorId.Others;

/// <summary>
///     Http response manager for remove major
///     temporarily by major id feature.
/// </summary>
internal sealed class RemoveMajorTemporarilyByMajorIdHttpResponseManager
{
    private readonly Dictionary<
        RemoveMajorTemporarilyByMajorIdResponseStatusCode,
        Func<
            RemoveMajorTemporarilyByMajorIdRequest,
            RemoveMajorTemporarilyByMajorIdResponse,
            IRemoveMajorTemporarilyByMajorIdHttpResponse>>
                _dictionary;

    internal RemoveMajorTemporarilyByMajorIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RemoveMajorTemporarilyByMajorIdResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: RemoveMajorTemporarilyByMajorIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, _) => new OperationSuccessHttpResponse());

        _dictionary.Add(
            key: RemoveMajorTemporarilyByMajorIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, _) => new DatabaseOperationFailHttpResponse());

        _dictionary.Add(
            key: RemoveMajorTemporarilyByMajorIdResponseStatusCode.MAJOR_IS_NOT_FOUND,
            value: (request, _) => new MajorIsNotFoundHttpResponse(request: request));

        _dictionary.Add(
            key: RemoveMajorTemporarilyByMajorIdResponseStatusCode.MAJOR_IS_ALREADY_TEMPORARILY_REMOVED,
            value: (request, _) => new MajorIsAlreadyTemporarilyRemovedHttpResponse(request: request));
    }

    internal Func<
        RemoveMajorTemporarilyByMajorIdRequest,
        RemoveMajorTemporarilyByMajorIdResponse,
        IRemoveMajorTemporarilyByMajorIdHttpResponse>
            Resolve(RemoveMajorTemporarilyByMajorIdResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}
