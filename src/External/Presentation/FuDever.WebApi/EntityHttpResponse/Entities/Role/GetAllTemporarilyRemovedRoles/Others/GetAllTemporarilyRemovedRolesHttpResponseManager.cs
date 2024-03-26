using System.Collections.Generic;
using System;
using FuDever.Application.Features.Role.GetAllTemporarilyRemovedRoles;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Role.GetAllTemporarilyRemovedRoles.Others;

/// <summary>
///     Http response manager for get all
///     temporarily removed roles feature.
/// </summary>
internal sealed class GetAllTemporarilyRemovedRolesHttpResponseManager
{
    private readonly Dictionary<
        GetAllTemporarilyRemovedRolesResponseStatusCode,
        Func<
            GetAllTemporarilyRemovedRolesRequest,
            GetAllTemporarilyRemovedRolesResponse,
            IGetAllTemporarilyRemovedRolesHttpResponse>>
                _dictionary;

    internal GetAllTemporarilyRemovedRolesHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllTemporarilyRemovedRolesResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: GetAllTemporarilyRemovedRolesResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) => new OperationSuccessHttpResponse(response: response));
    }

    internal Func<
        GetAllTemporarilyRemovedRolesRequest,
        GetAllTemporarilyRemovedRolesResponse,
        IGetAllTemporarilyRemovedRolesHttpResponse>
            Resolve(GetAllTemporarilyRemovedRolesResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}