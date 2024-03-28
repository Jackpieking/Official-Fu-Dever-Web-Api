using FuDever.Application.Features.Major.CreateMajor;
using System;
using System.Collections.Generic;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Major.CreateMajor.Others;

/// <summary>
///     Http response manager for create major feature.
/// </summary>
internal sealed class CreateMajorHttpResponseManager
{
    private readonly Dictionary<
        CreateMajorResponseStatusCode,
        Func<
            CreateMajorRequest,
            CreateMajorResponse,
            ICreateMajorHttpResponse>>
                _dictionary;

    internal CreateMajorHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: CreateMajorResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: CreateMajorResponseStatusCode.OPERATION_SUCCESS,
            value: (_, _) => new OperationSuccessHttpResponse());

        _dictionary.Add(
            key: CreateMajorResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, _) => new DatabaseOperationFailHttpResponse());

        _dictionary.Add(
            key: CreateMajorResponseStatusCode.MAJOR_IS_ALREADY_TEMPORARILY_REMOVED,
            value: (request, _) => new MajorIsAlreadyTemporarilyRemovedHttpResponse(request: request));

        _dictionary.Add(
            key: CreateMajorResponseStatusCode.MAJOR_ALREADY_EXISTS,
            value: (request, _) => new MajorAlreadyExistsHttpResponse(request: request));
    }

    internal Func<
        CreateMajorRequest,
        CreateMajorResponse,
        ICreateMajorHttpResponse>
            Resolve(CreateMajorResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}
