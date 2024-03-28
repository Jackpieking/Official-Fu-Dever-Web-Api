using System.Collections.Generic;
using System;
using FuDever.Application.Features.Major.RemoveMajorPermanentlyByMajorId;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Major.RemoveMajorPermanentlyByMajorId.Others;

/// <summary>
///     Http response manager for remove major
///     permanently by major id feature.
/// </summary>
internal sealed class RemoveMajorPermanentlyByMajorIdHttpResponseManager
{
    private readonly Dictionary<
        RemoveMajorPermanentlyByMajorIdResponseStatusCode,
        Func<
            RemoveMajorPermanentlyByMajorIdRequest,
            RemoveMajorPermanentlyByMajorIdResponse,
            IRemoveMajorPermanentlyByMajorIdHttpResponse>>
                _dictionary;

    internal RemoveMajorPermanentlyByMajorIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RemoveMajorPermanentlyByMajorIdResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: RemoveMajorPermanentlyByMajorIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, _) => new OperationSuccessHttpResponse());

        _dictionary.Add(
            key: RemoveMajorPermanentlyByMajorIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, _) => new DatabaseOperationFailHttpResponse());

        _dictionary.Add(
            key: RemoveMajorPermanentlyByMajorIdResponseStatusCode.MAJOR_IS_NOT_FOUND,
            value: (request, _) => new MajorIsNotFoundHttpResponse(request: request));

        _dictionary.Add(
            key: RemoveMajorPermanentlyByMajorIdResponseStatusCode.MAJOR_IS_NOT_TEMPORARILY_REMOVED,
            value: (request, _) => new MajorIsNotTemporarilyRemovedHttpResponse(request: request));
    }

    internal Func<
        RemoveMajorPermanentlyByMajorIdRequest,
        RemoveMajorPermanentlyByMajorIdResponse,
        IRemoveMajorPermanentlyByMajorIdHttpResponse>
            Resolve(RemoveMajorPermanentlyByMajorIdResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}
