using System.Collections.Generic;
using System;
using FuDever.Application.Features.Hobby.GetAllHobbiesByHobbyName;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Hobby.GetAllHobbiesByHobbyName.Others;

/// <summary>
///     Http response manager for get all hobbies
///     by hobby name feature.
/// </summary>
internal sealed class GetAllHobbiesByHobbyNameHttpResponseManager
{
    private readonly Dictionary<
        GetAllHobbiesByHobbyNameResponseStatusCode,
        Func<
            GetAllHobbiesByHobbyNameRequest,
            GetAllHobbiesByHobbyNameResponse,
            IGetAllHobbiesByHobbyNameHttpResponse>>
                _dictionary;

    internal GetAllHobbiesByHobbyNameHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllHobbiesByHobbyNameResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: GetAllHobbiesByHobbyNameResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) => new OperationSuccessHttpResponse(response: response));
    }

    internal Func<
        GetAllHobbiesByHobbyNameRequest,
        GetAllHobbiesByHobbyNameResponse,
        IGetAllHobbiesByHobbyNameHttpResponse>
            Resolve(GetAllHobbiesByHobbyNameResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}
