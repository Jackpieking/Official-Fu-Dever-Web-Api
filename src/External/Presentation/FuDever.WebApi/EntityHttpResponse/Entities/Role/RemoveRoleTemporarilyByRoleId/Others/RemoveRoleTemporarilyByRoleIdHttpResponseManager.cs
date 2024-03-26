using System.Collections.Generic;
using System;
using FuDever.Application.Features.Role.RemoveRoleTemporarilyByRoleId;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Role.RemoveRoleTemporarilyByRoleId.Others;

/// <summary>
///     Http response manager for remove role
///     temporarily by role id feature.
/// </summary>
internal sealed class RemoveRoleTemporarilyByRoleIdHttpResponseManager
{
    private readonly Dictionary<
        RemoveRoleTemporarilyByRoleIdResponseStatusCode,
        Func<
            RemoveRoleTemporarilyByRoleIdRequest,
            RemoveRoleTemporarilyByRoleIdResponse,
            IRemoveRoleTemporarilyByRoleIdHttpResponse>>
                _dictionary;

    internal RemoveRoleTemporarilyByRoleIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RemoveRoleTemporarilyByRoleIdResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: RemoveRoleTemporarilyByRoleIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, _) => new OperationSuccessHttpResponse());

        _dictionary.Add(
            key: RemoveRoleTemporarilyByRoleIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, _) => new DatabaseOperationFailHttpResponse());

        _dictionary.Add(
            key: RemoveRoleTemporarilyByRoleIdResponseStatusCode.ROLE_IS_NOT_FOUND,
            value: (request, _) => new RoleIsNotFoundHttpResponse(request: request));

        _dictionary.Add(
            key: RemoveRoleTemporarilyByRoleIdResponseStatusCode.ROLE_IS_ALREADY_TEMPORARILY_REMOVED,
            value: (request, _) => new RoleIsAlreadyTemporarilyRemovedHttpResponse(request: request));
    }

    internal Func<
        RemoveRoleTemporarilyByRoleIdRequest,
        RemoveRoleTemporarilyByRoleIdResponse,
        IRemoveRoleTemporarilyByRoleIdHttpResponse>
            Resolve(RemoveRoleTemporarilyByRoleIdResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}
