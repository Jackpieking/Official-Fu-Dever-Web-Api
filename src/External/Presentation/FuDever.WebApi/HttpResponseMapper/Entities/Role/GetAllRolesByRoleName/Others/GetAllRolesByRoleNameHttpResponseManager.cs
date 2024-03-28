using System.Collections.Generic;
using System;
using FuDever.Application.Features.Role.GetAllRolesByRoleName;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Role.GetAllRolesByRoleName.Others;

/// <summary>
///     Http response manager for get all roles
///     by role name feature.
/// </summary>
internal sealed class GetAllRolesByRoleNameHttpResponseManager
{
    private readonly Dictionary<
        GetAllRolesByRoleNameResponseStatusCode,
        Func<
            GetAllRolesByRoleNameRequest,
            GetAllRolesByRoleNameResponse,
            IGetAllRolesByRoleNameHttpResponse>>
                _dictionary;

    internal GetAllRolesByRoleNameHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllRolesByRoleNameResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: GetAllRolesByRoleNameResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) => new OperationSuccessHttpResponse(response: response));
    }

    internal Func<
        GetAllRolesByRoleNameRequest,
        GetAllRolesByRoleNameResponse,
        IGetAllRolesByRoleNameHttpResponse>
            Resolve(GetAllRolesByRoleNameResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}
