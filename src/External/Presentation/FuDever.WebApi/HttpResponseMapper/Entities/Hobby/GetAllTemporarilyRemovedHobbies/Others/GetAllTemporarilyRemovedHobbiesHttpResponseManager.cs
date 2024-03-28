using System.Collections.Generic;
using System;
using FuDever.Application.Features.Hobby.GetAllTemporarilyRemovedHobbies;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Hobby.GetAllTemporarilyRemovedHobbies.Others;

/// <summary>
///     Http response manager for get all
///     temporarily removed hobbies feature.
/// </summary>
internal sealed class GetAllTemporarilyRemovedHobbiesHttpResponseManager
{
    private readonly Dictionary<
        GetAllTemporarilyRemovedHobbiesResponseStatusCode,
        Func<
            GetAllTemporarilyRemovedHobbiesRequest,
            GetAllTemporarilyRemovedHobbiesResponse,
            IGetAllTemporarilyRemovedHobbiesHttpResponse>>
                _dictionary;

    internal GetAllTemporarilyRemovedHobbiesHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllTemporarilyRemovedHobbiesResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: GetAllTemporarilyRemovedHobbiesResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) => new OperationSuccessHttpResponse(response: response));
    }

    internal Func<
        GetAllTemporarilyRemovedHobbiesRequest,
        GetAllTemporarilyRemovedHobbiesResponse,
        IGetAllTemporarilyRemovedHobbiesHttpResponse>
            Resolve(GetAllTemporarilyRemovedHobbiesResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}