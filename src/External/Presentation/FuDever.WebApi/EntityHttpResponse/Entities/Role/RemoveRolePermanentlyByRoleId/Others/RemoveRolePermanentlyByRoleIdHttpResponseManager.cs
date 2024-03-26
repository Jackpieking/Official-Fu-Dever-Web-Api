using System.Collections.Generic;
using System;
using FuDever.Application.Features.Role.RemoveRolePermanentlyByRoleId;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Role.RemoveRolePermanentlyByRoleId.Others;

/// <summary>
///     Http response manager for remove role
///     permanently by role id feature.
/// </summary>
internal sealed class RemoveRolePermanentlyByRoleIdHttpResponseManager
{
    private readonly Dictionary<
        RemoveRolePermanentlyByRoleIdResponseStatusCode,
        Func<
            RemoveRolePermanentlyByRoleIdRequest,
            RemoveRolePermanentlyByRoleIdResponse,
            IRemoveRolePermanentlyByRoleIdHttpResponse>>
                _dictionary;

    internal RemoveRolePermanentlyByRoleIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RemoveRolePermanentlyByRoleIdResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: RemoveRolePermanentlyByRoleIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, _) => new OperationSuccessHttpResponse());

        _dictionary.Add(
            key: RemoveRolePermanentlyByRoleIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, _) => new DatabaseOperationFailHttpResponse());

        _dictionary.Add(
            key: RemoveRolePermanentlyByRoleIdResponseStatusCode.ROLE_IS_NOT_FOUND,
            value: (request, _) => new RoleIsNotFoundHttpResponse(request: request));

        _dictionary.Add(
            key: RemoveRolePermanentlyByRoleIdResponseStatusCode.ROLE_IS_NOT_TEMPORARILY_REMOVED,
            value: (request, _) => new RoleIsNotTemporarilyRemovedHttpResponse(request: request));
    }

    internal Func<
        RemoveRolePermanentlyByRoleIdRequest,
        RemoveRolePermanentlyByRoleIdResponse,
        IRemoveRolePermanentlyByRoleIdHttpResponse>
            Resolve(RemoveRolePermanentlyByRoleIdResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}
