using System.Collections.Generic;
using System;
using FuDever.Application.Features.Role.UpdateRoleByRoleId;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Role.UpdateRoleByRoleId.Others;

/// <summary>
///     Http response manager for update role
///     by role id feature.
/// </summary>
internal sealed class UpdateRoleByRoleIdHttpResponseManager
{
    private readonly Dictionary<
    UpdateRoleByRoleIdResponseStatusCode,
    Func<
        UpdateRoleByRoleIdRequest,
        UpdateRoleByRoleIdResponse,
        IUpdateRoleByRoleIdHttpResponse>>
            _dictionary;

    internal UpdateRoleByRoleIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: UpdateRoleByRoleIdResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: UpdateRoleByRoleIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, _) => new OperationSuccessHttpResponse());

        _dictionary.Add(
            key: UpdateRoleByRoleIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, _) => new DatabaseOperationFailHttpResponse());

        _dictionary.Add(
            key: UpdateRoleByRoleIdResponseStatusCode.ROLE_IS_ALREADY_TEMPORARILY_REMOVED,
            value: (request, _) => new RoleIsAlreadyTemporarilyRemovedHttpResponse(request: request));

        _dictionary.Add(
            key: UpdateRoleByRoleIdResponseStatusCode.ROLE_ALREADY_EXISTS,
            value: (request, _) => new RoleAlreadyExistsHttpResponse(request: request));

        _dictionary.Add(
            key: UpdateRoleByRoleIdResponseStatusCode.ROLE_IS_NOT_FOUND,
            value: (request, _) => new RoleIsNotFoundHttpResponse(request: request));
    }

    internal Func<
        UpdateRoleByRoleIdRequest,
        UpdateRoleByRoleIdResponse,
        IUpdateRoleByRoleIdHttpResponse>
            Resolve(UpdateRoleByRoleIdResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}
