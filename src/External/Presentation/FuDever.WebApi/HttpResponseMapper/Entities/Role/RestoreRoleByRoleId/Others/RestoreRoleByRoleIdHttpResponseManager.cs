using System.Collections.Generic;
using System;
using FuDever.Application.Features.Role.RestoreRoleByRoleId;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Role.RestoreRoleByRoleId.Others;

/// <summary>
///     Http response manager for restore role
///     by role id feature.
/// </summary>
internal sealed class RestoreRoleByRoleIdHttpResponseManager
{
    private readonly Dictionary<
    RestoreRoleByRoleIdResponseStatusCode,
    Func<
        RestoreRoleByRoleIdRequest,
        RestoreRoleByRoleIdResponse,
        IRestoreRoleByRoleIdHttpResponse>>
            _dictionary;

    internal RestoreRoleByRoleIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RestoreRoleByRoleIdResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: RestoreRoleByRoleIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, _) => new OperationSuccessHttpResponse());

        _dictionary.Add(
            key: RestoreRoleByRoleIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, _) => new DatabaseOperationFailHttpResponse());

        _dictionary.Add(
            key: RestoreRoleByRoleIdResponseStatusCode.ROLE_IS_NOT_FOUND,
            value: (request, _) => new RoleIsNotFoundHttpResponse(request: request));

        _dictionary.Add(
            key: RestoreRoleByRoleIdResponseStatusCode.ROLE_IS_NOT_TEMPORARILY_REMOVED,
            value: (request, _) => new RoleIsNotTemporarilyRemovedHttpResponse(request: request));
    }

    internal Func<
        RestoreRoleByRoleIdRequest,
        RestoreRoleByRoleIdResponse,
        IRestoreRoleByRoleIdHttpResponse>
            Resolve(RestoreRoleByRoleIdResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}
