using FuDever.Application.Features.Hobby.GetAllHobbies;
using System;
using System.Collections.Generic;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Hobby.GetAllHobbies.Others;

/// <summary>
///     Http response manager for get all hobbies feature.
/// </summary>
internal sealed class GetAllHobbiesHttpResponseManager
{
    private readonly Dictionary<
        GetAllHobbiesResponseStatusCode,
        Func<
            GetAllHobbiesRequest,
            GetAllHobbiesResponse,
            IGetAllHobbiesHttpResponse>>
                _dictionary;

    internal GetAllHobbiesHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllHobbiesResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: GetAllHobbiesResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) => new OperationSuccessHttpResponse(response: response));
    }

    internal Func<
        GetAllHobbiesRequest,
        GetAllHobbiesResponse,
        IGetAllHobbiesHttpResponse>
            Resolve(GetAllHobbiesResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}
