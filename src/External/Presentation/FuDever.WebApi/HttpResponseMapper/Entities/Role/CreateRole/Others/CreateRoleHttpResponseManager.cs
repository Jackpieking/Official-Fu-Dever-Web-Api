using FuDever.Application.Features.Role.CreateRole;
using System;
using System.Collections.Generic;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Role.CreateRole.Others;

/// <summary>
///     Http response manager for create role feature.
/// </summary>
internal sealed class CreateRoleHttpResponseManager
{
    private readonly Dictionary<
        CreateRoleResponseStatusCode,
        Func<
            CreateRoleRequest,
            CreateRoleResponse,
            ICreateRoleHttpResponse>>
                _dictionary;

    internal CreateRoleHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: CreateRoleResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: CreateRoleResponseStatusCode.OPERATION_SUCCESS,
            value: (_, _) => new OperationSuccessHttpResponse());

        _dictionary.Add(
            key: CreateRoleResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, _) => new DatabaseOperationFailHttpResponse());

        _dictionary.Add(
            key: CreateRoleResponseStatusCode.ROLE_IS_ALREADY_TEMPORARILY_REMOVED,
            value: (request, _) => new RoleIsAlreadyTemporarilyRemovedHttpResponse(request: request));

        _dictionary.Add(
            key: CreateRoleResponseStatusCode.ROLE_ALREADY_EXISTS,
            value: (request, _) => new RoleAlreadyExistsHttpResponse(request: request));
    }

    internal Func<
        CreateRoleRequest,
        CreateRoleResponse,
        ICreateRoleHttpResponse>
            Resolve(CreateRoleResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}
