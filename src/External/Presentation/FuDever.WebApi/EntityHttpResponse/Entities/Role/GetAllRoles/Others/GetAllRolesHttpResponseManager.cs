using FuDever.Application.Features.Role.GetAllRoles;
using System;
using System.Collections.Generic;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Role.GetAllRoles.Others;

/// <summary>
///     Http response manager for get all roles feature.
/// </summary>
internal sealed class GetAllRolesHttpResponseManager
{
    private readonly Dictionary<
        GetAllRolesResponseStatusCode,
        Func<
            GetAllRolesRequest,
            GetAllRolesResponse,
            IGetAllRolesHttpResponse>>
                _dictionary;

    internal GetAllRolesHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllRolesResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: GetAllRolesResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) => new OperationSuccessHttpResponse(response: response));
    }

    internal Func<
        GetAllRolesRequest,
        GetAllRolesResponse,
        IGetAllRolesHttpResponse>
            Resolve(GetAllRolesResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}
