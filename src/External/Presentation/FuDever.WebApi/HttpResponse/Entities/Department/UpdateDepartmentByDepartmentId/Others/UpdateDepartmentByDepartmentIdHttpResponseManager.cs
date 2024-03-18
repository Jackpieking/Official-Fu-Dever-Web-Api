using System.Collections.Generic;
using System;
using FuDever.Application.Features.Department.UpdateDepartmentByDepartmentId;

namespace FuDever.WebApi.HttpResponse.Entities.Department.UpdateDepartmentByDepartmentId.Others;

/// <summary>
///     Http response manager for update department
///     by department id feature.
/// </summary>
internal sealed class UpdateDepartmentByDepartmentIdHttpResponseManager
{
    private readonly Dictionary<
    UpdateDepartmentByDepartmentIdResponseStatusCode,
    Func<
        UpdateDepartmentByDepartmentIdRequest,
        UpdateDepartmentByDepartmentIdResponse,
        IUpdateDepartmentByDepartmentIdHttpResponse>>
            _dictionary;

    internal UpdateDepartmentByDepartmentIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: UpdateDepartmentByDepartmentIdResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: UpdateDepartmentByDepartmentIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, _) => new OperationSuccessHttpResponse());

        _dictionary.Add(
            key: UpdateDepartmentByDepartmentIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, _) => new DatabaseOperationFailHttpResponse());

        _dictionary.Add(
            key: UpdateDepartmentByDepartmentIdResponseStatusCode.DEPARTMENT_IS_ALREADY_TEMPORARILY_REMOVED,
            value: (request, _) => new DepartmentIsAlreadyTemporarilyRemovedHttpResponse(request: request));

        _dictionary.Add(
            key: UpdateDepartmentByDepartmentIdResponseStatusCode.DEPARTMENT_ALREADY_EXISTS,
            value: (request, _) => new DepartmentAlreadyExistsHttpResponse(request: request));

        _dictionary.Add(
            key: UpdateDepartmentByDepartmentIdResponseStatusCode.DEPARTMENT_IS_NOT_FOUND,
            value: (request, _) => new DepartmentIsNotFoundHttpResponse(request: request));
    }

    internal Func<
        UpdateDepartmentByDepartmentIdRequest,
        UpdateDepartmentByDepartmentIdResponse,
        IUpdateDepartmentByDepartmentIdHttpResponse>
            Resolve(UpdateDepartmentByDepartmentIdResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}
