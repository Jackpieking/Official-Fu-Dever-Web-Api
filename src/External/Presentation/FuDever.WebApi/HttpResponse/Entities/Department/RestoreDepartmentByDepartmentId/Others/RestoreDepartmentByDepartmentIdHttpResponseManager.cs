using System.Collections.Generic;
using System;
using FuDever.Application.Features.Department.RestoreDepartmentByDepartmentId;

namespace FuDever.WebApi.HttpResponse.Entities.Department.RestoreDepartmentByDepartmentId.Others;

/// <summary>
///     Http response manager for restore department
///     by department id feature.
/// </summary>
internal sealed class RestoreDepartmentByDepartmentIdHttpResponseManager
{
    private readonly Dictionary<
    RestoreDepartmentByDepartmentIdResponseStatusCode,
    Func<
        RestoreDepartmentByDepartmentIdRequest,
        RestoreDepartmentByDepartmentIdResponse,
        IRestoreDepartmentByDepartmentIdHttpResponse>>
            _dictionary;

    internal RestoreDepartmentByDepartmentIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RestoreDepartmentByDepartmentIdResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: RestoreDepartmentByDepartmentIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, _) => new OperationSuccessHttpResponse());

        _dictionary.Add(
            key: RestoreDepartmentByDepartmentIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, _) => new DatabaseOperationFailHttpResponse());

        _dictionary.Add(
            key: RestoreDepartmentByDepartmentIdResponseStatusCode.DEPARTMENT_IS_NOT_FOUND,
            value: (request, _) => new DepartmentIsNotFoundHttpResponse(request: request));

        _dictionary.Add(
            key: RestoreDepartmentByDepartmentIdResponseStatusCode.DEPARTMENT_IS_NOT_TEMPORARILY_REMOVED,
            value: (request, _) => new DepartmentIsNotTemporarilyRemovedHttpResponse(request: request));
    }

    internal Func<
        RestoreDepartmentByDepartmentIdRequest,
        RestoreDepartmentByDepartmentIdResponse,
        IRestoreDepartmentByDepartmentIdHttpResponse>
            Resolve(RestoreDepartmentByDepartmentIdResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}
