using FuDever.Application.Features.Hobby.CreateHobby;
using System;
using System.Collections.Generic;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Hobby.CreateHobby.Others;

/// <summary>
///     Http response manager for create hobby feature.
/// </summary>
internal sealed class CreateHobbyHttpResponseManager
{
    private readonly Dictionary<
        CreateHobbyResponseStatusCode,
        Func<
            CreateHobbyRequest,
            CreateHobbyResponse,
            ICreateHobbyHttpResponse>>
                _dictionary;

    internal CreateHobbyHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: CreateHobbyResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: CreateHobbyResponseStatusCode.OPERATION_SUCCESS,
            value: (_, _) => new OperationSuccessHttpResponse());

        _dictionary.Add(
            key: CreateHobbyResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, _) => new DatabaseOperationFailHttpResponse());

        _dictionary.Add(
            key: CreateHobbyResponseStatusCode.HOBBY_IS_ALREADY_TEMPORARILY_REMOVED,
            value: (request, _) => new HobbyIsAlreadyTemporarilyRemovedHttpResponse(request: request));

        _dictionary.Add(
            key: CreateHobbyResponseStatusCode.HOBBY_ALREADY_EXISTS,
            value: (request, _) => new HobbyAlreadyExistsHttpResponse(request: request));
    }

    internal Func<
        CreateHobbyRequest,
        CreateHobbyResponse,
        ICreateHobbyHttpResponse>
            Resolve(CreateHobbyResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}
